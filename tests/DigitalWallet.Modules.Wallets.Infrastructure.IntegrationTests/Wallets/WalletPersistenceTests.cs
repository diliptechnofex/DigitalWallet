using DigitalWallet.Modules.Wallets.Domain.Wallets;
using DigitalWallet.Modules.Wallets.Domain.Wallets.Aggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Infrastructure.IntegrationTests.Wallets
{
    public sealed class WalletPersistenceTests(WalletsPostgresFixture fixture) : IClassFixture<WalletsPostgresFixture>
    {
        [Fact]
        public async Task SaveWallet_ThenLoadWallet_PreservesState()
        {
            await using var dbContext = fixture.CreateDbContext();

            var customerId = CustomerId.From(Guid.NewGuid());

            var wallet = Wallet.Open(
                customerId,
                CurrencyCode.Create("AUD"),
                DateTimeOffset.UtcNow);

            wallet.Activate(DateTimeOffset.UtcNow);

            dbContext.Wallets.Add(wallet);

            await dbContext.SaveChangesAsync();

            await using var secondDbContext =
                fixture.CreateDbContext();

            var loadedWallet =
                await secondDbContext.Wallets
                    .SingleAsync(item => item.Id == wallet.Id);

            Assert.Equal(wallet.Id, loadedWallet.Id);
            Assert.Equal(customerId, loadedWallet.CustomerId);
            Assert.Equal("AUD", loadedWallet.Currency.Value);
            Assert.Equal(WalletStatus.Active, loadedWallet.Status);
        }

        [Fact]
        public async Task SaveWallet_WhenWalletWasModifiedByAnotherContext_ThrowsConcurrencyException()
        {
            WalletId walletId;

            var customerId = CustomerId.From(Guid.NewGuid());

            await using (var setupContext = fixture.CreateDbContext())
            {
                var wallet = Wallet.Open(
                    customerId,
                    CurrencyCode.Create("AUD"),
                    DateTimeOffset.UtcNow);

                wallet.Activate(DateTimeOffset.UtcNow);

                setupContext.Wallets.Add(wallet);

                await setupContext.SaveChangesAsync();

                walletId = wallet.Id;
            }

            await using var firstContext = fixture.CreateDbContext();

            await using var secondContext = fixture.CreateDbContext();

            var firstWallet = await firstContext.Wallets.SingleAsync(wallet => wallet.Id == walletId);

            var secondWallet = await secondContext.Wallets.SingleAsync(wallet => wallet.Id == walletId);

            firstWallet.Suspend(WalletSuspensionReason.Create("Compliance review."), DateTimeOffset.UtcNow);

            await firstContext.SaveChangesAsync();

            secondWallet.Close(DateTimeOffset.UtcNow);

            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(() => secondContext.SaveChangesAsync());
        }
    }
}
