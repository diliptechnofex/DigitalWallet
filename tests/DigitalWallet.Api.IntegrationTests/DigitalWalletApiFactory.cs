using DigitalWallet.Modules.Wallets.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace DigitalWallet.Api.IntegrationTests
{
    public sealed class DigitalWalletApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _postgres =
            new PostgreSqlBuilder()
                .WithImage("postgres:17-alpine")
                .WithDatabase("digitalwallet_api_tests")
                .WithUsername("postgres")
                .WithPassword("technofex@123")
                .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll<DbContextOptions<WalletsDbContext>>();

                services.AddDbContext<WalletsDbContext>(options =>
                {
                    options.UseNpgsql(_postgres.GetConnectionString());
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
