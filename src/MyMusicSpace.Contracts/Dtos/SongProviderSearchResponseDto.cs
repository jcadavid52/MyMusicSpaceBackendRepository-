namespace MyMusicSpace.Contracts.Dtos
{
    public record SongProviderSearchResponseDto(
        IReadOnlyList<SongProviderDto> Tracks,
        int Total);
}
