using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Application.Wallets.OpenWallet
{
    public sealed class OpenWalletCommandValidator : AbstractValidator<OpenWalletCommand>
    {
        public OpenWalletCommandValidator()
        {
            RuleFor(command => command.CustomerId)
                .NotEmpty()
                .WithErrorCode("wallet.customer_id_required")
                .WithMessage("Customer identifier is required.");

            RuleFor(command => command.Currency)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithErrorCode("wallet.currency_required")
                .WithMessage("Currency is required.")
                .Length(3)
                .WithErrorCode("wallet.invalid_currency")
                .WithMessage("Currency must contain exactly three letters.")
                .Matches("^[A-Za-z]{3}$")
                .WithErrorCode("wallet.invalid_currency")
                .WithMessage("Currency must contain exactly three letters.");
        }
    }
}
