namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Dtos
{
    public record ProviderMusicSearchQueryDto(
        string Search,
        int Limit);
}
