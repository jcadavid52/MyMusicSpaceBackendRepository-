namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Dtos
{
    public record ProviderMusicSearchResponseDto(
        IReadOnlyList<ProviderMusicTrackDto> Tracks,
        int Total);
}
