using Microsoft.Extensions.DependencyInjection;
using MyMusicSpace.Business.Services;

namespace MyMusicSpace.Business
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddScoped<ISongService, SongService>();

            return services;
        }
    }
}
