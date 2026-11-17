using MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Dtos;

namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Abstractions
{
    public interface IProviderMusicSearchService
    {
        Task<ProviderMusicSearchResponseDto> ExecuteAsync(ProviderMusicSearchQueryDto query, CancellationToken cancellationToken = default);
    }
}
