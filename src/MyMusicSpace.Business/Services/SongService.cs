using MyMusicSpace.Business.Exceptions;
using MyMusicSpace.Business.Mappers;
using MyMusicSpace.Business.Services.Dtos;
using MyMusicSpace.Data.Exceptions;
using MyMusicSpace.Data.Models;
using MyMusicSpace.Data.Persistence.SqlServer.EfCoreConfigurations;
using MyMusicSpace.Data.Persistence.UnitOfWork;
using MyMusicSpace.Data.Repositories;

namespace MyMusicSpace.Business.Services
{
    public class SongService : ISongService
    {
        private const string UserName = "UserTest";

        private readonly ISongRepository _songRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SongService(
            ISongRepository songRepository,
            IAlbumRepository albumRepository,
            IArtistRepository artistRepository,
            IUnitOfWork unitOfWork)
        {
            _songRepository = songRepository;
            _albumRepository = albumRepository;
            _artistRepository = artistRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SongDto> CreateAsync(
            SongCreateRequestDto request,
            CancellationToken cancellationToken = default)
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

            return _albumRepository.Create(AlbumMapper.toAlbum(request, UserName));
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

                var created = _artistRepository.Create(ArtistMapper.toArtist(request, UserName));
                artistsByProviderId[created.ProviderId] = created;
                artists.Add(created);
            }

            return artists;
        }
    }
}
