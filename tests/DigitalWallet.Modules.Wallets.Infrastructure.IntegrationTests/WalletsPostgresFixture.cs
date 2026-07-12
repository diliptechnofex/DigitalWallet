using DigitalWallet.Modules.Wallets.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Testcontainers.PostgreSql;

namespace DigitalWallet.Modules.Wallets.Infrastructure.IntegrationTests
{
    public sealed class WalletsPostgresFixture : IAsyncLifetime
    {
        private readonly PostgreSqlContainer _container =
            new PostgreSqlBuilder()
                .WithImage("postgres:18-alpine")
                .WithDatabase("digitalwallet_tests")
                .WithUsername("postgres")
                .WithPassword("technofex@123")
                .Build();

        public string ConnectionString => _container.GetConnectionString();

        public async Task InitializeAsync()
        {
            await _container.StartAsync();

            await using var dbContext = CreateDbContext();

            await dbContext.Database.MigrateAsync();
        }

        public async Task DisposeAsync()
        {
            await _container.DisposeAsync();
        }

        public WalletsDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<WalletsDbContext>().UseNpgsql(ConnectionString).Options;

            return new WalletsDbContext(options);
        }
    }
}
