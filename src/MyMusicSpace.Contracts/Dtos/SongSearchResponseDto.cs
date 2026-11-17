namespace MyMusicSpace.Contracts.Dtos
{
    public record SongSearchResponseDto(
        IReadOnlyList<SongSearchResultDto> Tracks,
        int Total);
}
