using MyMusicSpace.Contracts.Dtos;
using MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Spotify.Dtos;

namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Spotify
{
    internal static class SpotifyTrackMapper
    {
        public static SongProviderDto ToProviderDto(SpotifyTrackDto track) =>
            new(
                track.Id,
                track.Name,
                track.ExternalUrls.Spotify,
                track.DurationMs,
                new AlbumProviderDto(
                    track.Album.Id,
                    track.Album.Name,
                    track.Album.Images?.FirstOrDefault()?.Url),
                track.Artists
                    .Select(artist => new ArtistProviderDto(artist.Id, artist.Name))
                    .ToArray());
    }
}
