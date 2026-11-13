using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyMusicSpace.Data.Persistence;
using MyMusicSpace.Data.Persistence.SqlServer;
using MyMusicSpace.Data.Persistence.SqlServer.HostedServices;
using MyMusicSpace.Data.Repositories;

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

            AddDatabaseOptions(services, configuration);
            AddSqlServerDatabase(services);
            AddRepositories(services);
            AddPersistenceHostedServices(services);

            return services;
        }

        private static void AddDatabaseOptions(
            IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddOptions<DatabaseOptions>()
                .Bind(configuration.GetSection(DatabaseOptions.SectionName))
                .ValidateOnStart();
        }

        private static void AddSqlServerDatabase(IServiceCollection services)
        {
            services.AddDbContext<SongSqlServerDbContext>((serviceProvider, options) =>
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
                    sqlServer.MigrationsAssembly(typeof(SongSqlServerDbContext).Assembly.FullName);
                });
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ISongRepository, SongSqlServerRepository>();
        }

        private static void AddPersistenceHostedServices(IServiceCollection services)
        {
            services.AddHostedService<AutomaticMigrationsHostedService>();
        }
    }
}
