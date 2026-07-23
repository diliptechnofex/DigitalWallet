using DigitalWallet.Modules.Wallets.Infrastructure.Persistence;
using Testcontainers.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace DigitalWallet.Api.IntegrationTests
{
    public sealed class DigitalWalletApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _postgres =
            new PostgreSqlBuilder()
                .WithImage("postgres:17-alpine")
                .WithDatabase("digitalwallet_api_tests")
                .WithUsername("wallet_app")
                .WithPassword("wallet_password")
                .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                configurationBuilder.AddInMemoryCollection(
                    new Dictionary<string, string?>
                    {
                        ["ConnectionStrings:WalletsDatabase"] =
                            _postgres.GetConnectionString()
                    });
            });
        }

        public async Task InitializeAsync()
        {
            await _postgres.StartAsync();

            using var scope = Services.CreateScope();

            var dbContext =
                scope.ServiceProvider.GetRequiredService<WalletsDbContext>();

            await dbContext.Database.MigrateAsync();
        }

        public new async Task DisposeAsync()
        {
            await _postgres.DisposeAsync();
        }
    }
}
