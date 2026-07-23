using DigitalWallet.Modules.Wallets.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Application.Wallets.ActivateWallet
{
    public sealed record ActivateWalletCommand(Guid WalletId) : IRequest<Result<ActivateWalletResponse>>;
}
