namespace MyMusicSpace.Contracts.Dtos
{
    public record AlbumProviderSearchResultDto(
        string ProviderId,
        string Name,
        string? ImageUrl);
}
