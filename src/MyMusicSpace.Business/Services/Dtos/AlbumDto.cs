namespace MyMusicSpace.Business.Services.Dtos
{
    public record AlbumDto(
        Guid Id,
        string ProviderId,
        string Name,
        string ImageUrl);
}
