using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Presentation.Wallets.Contracts.OpenWallet
{
    public sealed record OpenWalletRequest(
    Guid CustomerId,
    string Currency);
}
