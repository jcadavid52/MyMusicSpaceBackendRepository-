using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MyMusicSpace.Contracts;
using MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService;
using MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Abstractions;
using MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Spotify;
using MyMusicSpace.Data.Persistence;
using MyMusicSpace.Data.Persistence.Abstractions;
using MyMusicSpace.Data.Persistence.SqlServer;
using MyMusicSpace.Data.Persistence.SqlServer.HostedServices;
using MyMusicSpace.Data.Persistence.SqlServer.Repositories;

namespace MyMusicSpace.Data
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddDataServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configuration);

            AddOptions(services, configuration);
            AddExternalServiceClients(services);
            services.AddMemoryCache();
            AddSqlServerDatabase(services);
            AddRepositories(services);
            AddPersistenceHostedServices(services);

            return services;
        }

        private static void AddOptions(
            IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddOptions<DatabaseOptions>()
                .Bind(configuration.GetSection(DatabaseOptions.SectionName))
                .ValidateOnStart();

            services.AddOptions<ProviderMusicOptions>()
                .Bind(configuration.GetSection(ProviderMusicOptions.SectionName))
                .Validate(
                    options => !string.IsNullOrWhiteSpace(options.Spotify?.TokenUrl),
                    $"{ProviderMusicOptions.SectionName}:Spotify:TokenUrl is required.")
                .Validate(
                    options => !string.IsNullOrWhiteSpace(options.Spotify?.ClientId),
                    $"{ProviderMusicOptions.SectionName}:Spotify:ClientId is required.")
                .Validate(
                    options => !string.IsNullOrWhiteSpace(options.Spotify?.ClientSecret),
                    $"{ProviderMusicOptions.SectionName}:Spotify:ClientSecret is required.")
                .ValidateOnStart();
        }

        private static void AddSqlServerDatabase(IServiceCollection services)
        {
            services.AddDbContext<SpaceMusicSqlServerDbContext>((serviceProvider, options) =>
            {
                var connectionString = serviceProvider
                    .GetRequiredService<IConfiguration>()
                    .GetConnectionString(ConnectionStringNames.Default);

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException(
                        $"Connection string '{ConnectionStringNames.Default}' is not configured.");
                }

                options.UseSqlServer(connectionString, sqlServer =>
                {
                    sqlServer.EnableRetryOnFailure();
                    sqlServer.MigrationsAssembly(typeof(SpaceMusicSqlServerDbContext).Assembly.FullName);
                });
            });
            services.AddScoped<IUnitOfWork, SqlServerUnitOfWork>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ISongRepository, SongSqlServerRepository>();
            services.AddScoped<IAlbumRepository, AlbumSqlServerRepository>();
            services.AddScoped<IArtistRepository, ArtistSqlServerRepository>();
        }

        private static void AddPersistenceHostedServices(IServiceCollection services)
        {
            services.AddHostedService<AutomaticMigrationsHostedService>();
        }

        private static void AddExternalServiceClients(IServiceCollection services)
        {
            services.AddHttpClient<ISongSearchService, SpotifySearchService>((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<ProviderMusicOptions>>().Value;
                client.BaseAddress = new Uri(options.Spotify.ApiUrl);
            });

            services.AddHttpClient<IProviderMusicAccessTokenService, SpotifyAccessTokenService>((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<ProviderMusicOptions>>().Value;
                client.BaseAddress = new Uri(options.Spotify.TokenUrl);
            });
        }
    }
}
