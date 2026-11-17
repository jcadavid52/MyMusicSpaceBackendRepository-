namespace MyMusicSpace.Contracts.Dtos
{
    public record SongProviderDto(
        string ProviderId,
        string Name,
        string ProviderUrlTrack,
        long Duration,
        AlbumProviderDto Album,
        IReadOnlyList<ArtistProviderDto> Artists);
}
