namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Abstractions
{
    public interface IProviderMusicAccessTokenService
    {
        Task<string> ExecuteAsync(CancellationToken cancellationToken = default);
    }
}
