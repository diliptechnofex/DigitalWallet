using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Domain.Wallets
{
    public enum WalletStatus
    {
        PendingActivation = 1,
        Active = 2,
        Suspended = 3,
        Closed = 4
    }
}
