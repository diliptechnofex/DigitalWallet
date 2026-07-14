using DigitalWallet.Modules.Wallets.Application.Results;
using DigitalWallet.Modules.Wallets.Application.Wallets.OpenWallet;
using DigitalWallet.Modules.Wallets.Domain.Wallets;
using DigitalWallet.Modules.Wallets.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Infrastructure.IntegrationTests.Wallets
{
    public sealed class OpenWalletMediatRTests(WalletsPostgresFixture fixture) : IClassFixture<WalletsPostgresFixture>
    {
        [Fact]
        public async Task Send_ValidOpenWalletCommand_PersistsWallet()
        {
            var services = CreateServices();

            await using var serviceProvider = services.BuildServiceProvider();

            await using var scope = serviceProvider.CreateAsyncScope();

            var sender = scope.ServiceProvider.GetRequiredService<ISender>();

            var result = await sender.Send(
                new OpenWalletCommand(
                    Guid.NewGuid(),
                    "AUD"));

            Assert.True(result.IsSuccess);

            var dbContext = scope.ServiceProvider.GetRequiredService<WalletsDbContext>();

            //var walletExists = await dbContext.Wallets.AnyAsync(wallet => wallet.Id.Value == result.Value.WalletId);

            //Assert.True(walletExists);
            var walletId = WalletId.From(result.Value.WalletId);

            var walletExists = await dbContext.Wallets.AnyAsync(w => w.Id == walletId);

            Assert.True(walletExists);
        }

        [Fact]
        public async Task Send_InvalidOpenWalletCommand_ReturnsValidationFailure()
        {
            var services = CreateServices();

            await using var serviceProvider = services.BuildServiceProvider();

            await using var scope = serviceProvider.CreateAsyncScope();

            var sender = scope.ServiceProvider.GetRequiredService<ISender>();

            var result = await sender.Send(
                new OpenWalletCommand(
                    Guid.Empty,
                    "12"));

            Assert.True(result.IsFailure);
            Assert.Contains(result.Errors, error => error.Type == ErrorType.Validation);
        }

        private IServiceCollection CreateServices()
        {
            var configuration =
                new ConfigurationBuilder()
                    .AddInMemoryCollection(
                        new Dictionary<string, string?>
                        {
                            ["ConnectionStrings:WalletsDatabase"] =
                                fixture.ConnectionString
                        })
                    .Build();

            var services = new ServiceCollection();

            services.AddWalletsInfrastructure(configuration);

            return services;
        }
    }
}
