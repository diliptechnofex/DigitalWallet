using DigitalWallet.Modules.Wallets.Domain.Wallets;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Presentation.Wallets.Contracts.ActivateWallet
{
    public sealed record ActivateWalletRequest(Guid WalletId);
    
}
