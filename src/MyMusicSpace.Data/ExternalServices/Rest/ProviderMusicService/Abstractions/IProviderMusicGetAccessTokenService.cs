namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Abstractions
{
    public interface IProviderMusicGetAccessTokenService
    {
        Task<string> ExecuteAsync();
    }
}
