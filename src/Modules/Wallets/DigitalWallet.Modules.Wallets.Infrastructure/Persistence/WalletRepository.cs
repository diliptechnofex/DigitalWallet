using DigitalWallet.Modules.Wallets.Application.Repository;
using DigitalWallet.Modules.Wallets.Domain.Wallets;
using DigitalWallet.Modules.Wallets.Domain.Wallets.Aggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Infrastructure.Persistence
{
    public sealed class WalletRepository(WalletsDbContext dbContext) : IWalletRepository
    {
        public async Task<Wallet?> GetByIdAsync(WalletId walletId, CancellationToken cancellationToken)
        {
            return await dbContext.Wallets.SingleOrDefaultAsync(wallet => wallet.Id == walletId, cancellationToken);
        }

        public async Task<bool> ExistsForCustomerAndCurrencyAsync(CustomerId customerId, CurrencyCode currency, CancellationToken cancellationToken)
        {
            return await dbContext.Wallets.AnyAsync(wallet =>
                                              wallet.CustomerId == customerId &&
                                              wallet.Currency == currency,
                                              cancellationToken);
        }

        public void Add(Wallet wallet)
        {
            ArgumentNullException.ThrowIfNull(wallet);

            dbContext.Wallets.Add(wallet);
        }
    }
}
