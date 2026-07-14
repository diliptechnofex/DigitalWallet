using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Application.Wallets.OpenWallet
{
    public sealed record OpenWalletResponse(
     Guid WalletId,
     Guid CustomerId,
     string Currency,
     string Status);
}
