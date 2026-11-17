namespace MyMusicSpace.Contracts.Dtos
{
    public record SongSearchResultDto(
        string ProviderId,
        string Name,
        string ProviderUrlTrack,
        long Duration,
        AlbumSearchResultDto Album,
        IReadOnlyList<ArtistSearchResultDto> Artists);
}
