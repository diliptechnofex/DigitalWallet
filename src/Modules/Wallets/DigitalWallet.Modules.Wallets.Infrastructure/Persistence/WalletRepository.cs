using DigitalWallet.Modules.Wallets.Application.Repository;
using DigitalWallet.Modules.Wallets.Domain.Wallets;
using DigitalWallet.Modules.Wallets.Domain.Wallets.Aggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Infrastructure.Persistence
{
    internal sealed class WalletRepository(WalletsDbContext dbContext) : IWalletRepository
    {
        public Task<Wallet?> GetByIdAsync(WalletId walletId, CancellationToken cancellationToken)
        {
            return dbContext.Wallets.SingleOrDefaultAsync(wallet => wallet.Id == walletId, cancellationToken);
        }

        public Task<bool> ExistsForCustomerAndCurrencyAsync(CustomerId customerId, CurrencyCode currency, CancellationToken cancellationToken)
        {
            return dbContext.Wallets.AnyAsync(wallet =>
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
