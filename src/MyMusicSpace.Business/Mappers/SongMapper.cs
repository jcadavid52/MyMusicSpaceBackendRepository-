using MyMusicSpace.Business.Services.Dtos;
using MyMusicSpace.Data.Models;

namespace MyMusicSpace.Business.Mappers
{
    internal static class SongMapper
    {
        public static Song ToEntity(SongCreateRequestDto request,
            string userName,
            Guid albumId)
        {
            var song = new Song
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Lyrics = request.Lyrics,
                Duration = request.Duration,
                ProviderId = request.ProviderId,
                ProviderUrlTrack = request.ProviderUrlTrack,
                Translation = request.Translation,
                CreationDate = DateTime.UtcNow,
                UserName = userName,
                AlbumId = albumId
            };

            return song;
        }

        public static SongDto ToDto(Song song, Album album, IReadOnlyList<Artist> artists) => new(
            song.Id,
            song.ProviderId,
            song.ProviderUrlTrack,
            song.Name,
            song.Lyrics,
            song.Translation,
            song.Duration,
            artists
                .Select(artist => new ArtistDto(artist.Id, artist.ProviderId, artist.Name))
                .ToList(),
            new AlbumDto(album.Id, album.ProviderId, album.Name, album.ImageUrl),
            // La tablatura se agregará con el endpoint de subida.
            Tablature: null);
    }
}
