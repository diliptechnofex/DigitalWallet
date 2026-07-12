using DigitalWallet.Modules.Wallets.Domain.Wallets;
using DigitalWallet.Modules.Wallets.Domain.Wallets.Aggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Application.Repository
{
    public interface IWalletRepository
    {
        Task<Wallet?> GetByIdAsync( WalletId walletId,CancellationToken cancellationToken);

        Task<bool> ExistsForCustomerAndCurrencyAsync(CustomerId customerId,CurrencyCode currency,CancellationToken cancellationToken);

        void Add(Wallet wallet);
    }
}
