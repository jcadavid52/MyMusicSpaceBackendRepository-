namespace MyMusicSpace.Business.Services.Dtos
{
    public record AlbumSearchResultDto(
        string ProviderId,
        string Name,
        string? ImageUrl);
}
