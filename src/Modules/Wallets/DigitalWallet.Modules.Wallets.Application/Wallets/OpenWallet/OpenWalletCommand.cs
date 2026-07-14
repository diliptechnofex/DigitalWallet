using DigitalWallet.Modules.Wallets.Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Application.Wallets.OpenWallet
{
    public sealed record OpenWalletCommand(Guid CustomerId, string Currency) : IRequest<Result<OpenWalletResponse>>;
}
