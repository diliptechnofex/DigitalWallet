using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Presentation.Wallets.Contracts.OpenWallet
{
    public sealed record OpenWalletApiResponse(
    Guid WalletId,
    Guid CustomerId,
    string Currency,
    string Status);
}
