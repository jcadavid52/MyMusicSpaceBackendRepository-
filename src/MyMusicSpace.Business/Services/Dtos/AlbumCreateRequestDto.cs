namespace MyMusicSpace.Business.Services.Dtos
{
    public record AlbumCreateRequestDto(
        string ProviderId,
        string Name,
        string ImageUrl);
}
