namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Dtos
{
    public record ProviderMusicTrackDto(
        string ProviderId,
        string Name,
        string ProviderUrlTrack,
        long DurationMs,
        ProviderMusicAlbumDto Album,
        IReadOnlyList<ProviderMusicArtistDto> Artists);
}
