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

        public static Error NotFound()
        {
            return Error.NotFound(
                "wallet.not_found",
                "Wallet was not found.");
        }

        public static Error InvalidStateTransition(string message)
        {
            return Error.Conflict(
                "wallet.invalid_state_transition",
                message);
        }
    }
}
