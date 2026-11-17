namespace MyMusicSpace.Contracts.Dtos
{
    public record SongProviderSearchResponseDto(
        IReadOnlyList<SongProviderSearchResultDto> Tracks,
        int Total);
}
