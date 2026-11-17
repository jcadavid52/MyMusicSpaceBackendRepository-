using Microsoft.Extensions.Logging;
using MyMusicSpace.Business.Exceptions;
using MyMusicSpace.Business.Mappers;
using MyMusicSpace.Business.Services.Abstractions;
using MyMusicSpace.Business.Services.Dtos;
using MyMusicSpace.Data.Exceptions;
using MyMusicSpace.Data.Persistence.Abstractions;
using MyMusicSpace.Data.Persistence.Models;
using MyMusicSpace.Data.Persistence.SqlServer.EfCoreConfigurations;

namespace MyMusicSpace.Business.Services
{
    public class SongCreateService : ISongCreateService
    {
        private const string UserName = "UserTest";
        private const int MaxAttempts = 3;

        private readonly ISongRepository _songRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SongCreateService> _logger;

        public SongCreateService(
            ISongRepository songRepository,
            IAlbumRepository albumRepository,
            IArtistRepository artistRepository,
            IUnitOfWork unitOfWork,
            ILogger<SongCreateService> logger)
        {
            _songRepository = songRepository;
            _albumRepository = albumRepository;
            _artistRepository = artistRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<SongDto> ExecuteAsync(
            SongCreateRequestDto request,
            CancellationToken cancellationToken = default)
        {
            for (var attempt = 1; ; attempt++)
            {
                try
                {
                    return await CreateCoreAsync(request, cancellationToken);
                }
                catch (UniqueConstraintViolationException ex)
                    when (attempt < MaxAttempts && IsSharedCatalogConflict(ex))
                {
                    // Otro request insertó el mismo Album/Artist entre el SELECT y el INSERT.
                    // El SaveChanges completo hizo rollback, pero el tracker conserva las
                    // entidades como Added: hay que limpiarlo antes de reconstruir el grafo.
                    // En el siguiente intento el SELECT ya encuentra la fila del ganador.
                    _logger.LogWarning(
                        "Conflicto de concurrencia en {Constraint} al crear la canción {ProviderId}. "
                        + "Reintento {Attempt}/{MaxAttempts}.",
                        ex.ConstraintName,
                        request.ProviderId,
                        attempt,
                        MaxAttempts);

                    _unitOfWork.ClearChanges();
                }
            }
        }

        // Solo Album y Artist pueden perder una carrera: son catálogos compartidos que
        // deduplicamos por (UserName, ProviderId). El índice de Songs no se reintenta
        // porque ahí el duplicado es real y se traduce a SongAlreadyExistsException.
        private static bool IsSharedCatalogConflict(UniqueConstraintViolationException exception) =>
            exception.ConstraintName is UniqueIndexes.AlbumUserProvider or UniqueIndexes.ArtistUserProvider;

        private async Task<SongDto> CreateCoreAsync(
            SongCreateRequestDto request,
            CancellationToken cancellationToken)
        {
            if (await _songRepository.ExistsAsync(UserName, request.ProviderId, cancellationToken))
            {
                throw new SongAlreadyExistsException(request.ProviderId);
            }

            var creationDate = DateTime.UtcNow;

            var album = await GetOrCreateAlbumAsync(request.Album, cancellationToken);
            var artists = await GetOrCreateArtistsAsync(request.Artists, cancellationToken);

            var song = SongMapper.ToEntity(request, UserName, album.Id);

            song.ArtistSongs = artists
                .Select(artist => new ArtistSong
                {
                    ArtistId = artist.Id,
                    SongId = song.Id,
                    CreationDate = creationDate,
                    UserName = UserName
                })
                .ToList();

            _songRepository.Create(song);

            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (UniqueConstraintViolationException ex)
                when (ex.ConstraintName == UniqueIndexes.SongUserProvider)
            {
                throw new SongAlreadyExistsException(request.ProviderId, ex);
            }

            return SongMapper.ToDto(song, album, artists);
        }

        private async Task<Album> GetOrCreateAlbumAsync(
            AlbumCreateRequestDto request,
            CancellationToken cancellationToken)
        {
            var existing = await _albumRepository.GetByProviderIdAsync(
                UserName,
                request.ProviderId,
                cancellationToken);

            if (existing is not null)
            {
                return existing;
            }

            return _albumRepository.Create(AlbumMapper.ToAlbum(request, UserName));
        }

        private async Task<IReadOnlyList<Artist>> GetOrCreateArtistsAsync(
            IEnumerable<ArtistCreateRequestDto> requestedArtists,
            CancellationToken cancellationToken)
        {
            var requests = requestedArtists.ToList();
            if (requests.Count == 0)
            {
                return [];
            }

            var comparer = StringComparer.OrdinalIgnoreCase;
            var providerIds = requests
                .Select(artist => artist.ProviderId)
                .Distinct(comparer)
                .ToList();

            var existingArtists = await _artistRepository.GetByProviderIdsAsync(
                UserName,
                providerIds,
                cancellationToken);

            var artistsByProviderId = existingArtists
                .ToDictionary(artist => artist.ProviderId, comparer);

            var artists = new List<Artist>(requests.Count);

            foreach (var request in requests)
            {
                if (artistsByProviderId.TryGetValue(request.ProviderId, out var artist))
                {
                    artists.Add(artist);
                    continue;
                }

                var created = _artistRepository.Create(ArtistMapper.ToArtist(request, UserName));
                artistsByProviderId[created.ProviderId] = created;
                artists.Add(created);
            }

            return artists;
        }
    }
}
