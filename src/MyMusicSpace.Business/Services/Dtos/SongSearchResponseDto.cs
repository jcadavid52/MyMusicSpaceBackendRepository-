namespace MyMusicSpace.Business.Services.Dtos
{
    public record SongSearchResponseDto(
        IReadOnlyList<SongSearchResultDto> Tracks,
        int Total);
}
