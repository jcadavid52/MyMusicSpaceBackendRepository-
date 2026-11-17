namespace MyMusicSpace.Contracts.Dtos
{
    public record AlbumSearchResultDto(
        string ProviderId,
        string Name,
        string? ImageUrl);
}
