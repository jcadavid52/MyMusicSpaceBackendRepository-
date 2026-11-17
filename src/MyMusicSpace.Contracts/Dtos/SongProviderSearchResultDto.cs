namespace MyMusicSpace.Contracts.Dtos
{
    public record SongProviderSearchResultDto(
        string ProviderId,
        string Name,
        string ProviderUrlTrack,
        long Duration,
        AlbumProviderSearchResultDto Album,
        IReadOnlyList<ArtistProviderSearchResultDto> Artists);
}
