using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Application.Wallets.ActivateWallet
{
    public sealed class ActivateWalletCommandValidator : AbstractValidator<ActivateWalletCommand>
    {
        public ActivateWalletCommandValidator()
        {
            RuleFor(command => command.WalletId)
                .NotEmpty()
                .WithErrorCode("wallet.wallet_id_required")
                .WithMessage("Wallet identifier is required.");
        }
    }
}
