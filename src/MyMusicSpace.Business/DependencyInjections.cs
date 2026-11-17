using Microsoft.Extensions.DependencyInjection;
using MyMusicSpace.Business.Services;
using MyMusicSpace.Business.Services.Abstractions;

namespace MyMusicSpace.Business
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddScoped<ISongCreateService, SongCreateService>();

            return services;
        }
    }
}
