using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MyMusicSpace.Data.Persistence.SqlServer.HostedServices
{
    public class AutomaticMigrationsHostedService : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly DatabaseOptions _options;
        private readonly ILogger<AutomaticMigrationsHostedService> _logger;

        public AutomaticMigrationsHostedService(
            IServiceScopeFactory scopeFactory,
            IOptions<DatabaseOptions> options,
            ILogger<AutomaticMigrationsHostedService> logger)
        {
            _scopeFactory = scopeFactory;
            _options = options.Value;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (!_options.AutomaticMigrations)
            {
                _logger.LogInformation(
                    "Automatic migrations are disabled (AutomaticSeedData={AutomaticSeedData}).",
                    _options.AutomaticSeedData);
                return;
            }

            await using var scope = _scopeFactory.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<SpaceMusicSqlServerDbContext>();

            _logger.LogInformation("Applying database migrations...");
            await dbContext.Database.MigrateAsync(cancellationToken);
            _logger.LogInformation("Database migrations applied successfully.");
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
