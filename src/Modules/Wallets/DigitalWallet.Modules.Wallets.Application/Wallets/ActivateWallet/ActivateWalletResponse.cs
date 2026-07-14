using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Application.Wallets.ActivateWallet
{
    public sealed record ActivateWalletResponse(
    Guid WalletId,
    string Status,
    DateTimeOffset? ActivatedAtUtc);
}
