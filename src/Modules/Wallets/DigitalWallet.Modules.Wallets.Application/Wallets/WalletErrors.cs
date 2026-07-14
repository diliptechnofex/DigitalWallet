using DigitalWallet.Modules.Wallets.Application.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Application.Wallets
{
    public static class WalletErrors
    {
        public static Error AlreadyExists(string currency)
        {
            return Error.Conflict(
                "wallet.already_exists",
                $"Customer already has a wallet in {currency}.");
        }
    }
}
