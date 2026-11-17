namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Dtos
{
    public record ProviderMusicAlbumDto(
        string ProviderId,
        string Name,
        string? ImageUrl);
}
