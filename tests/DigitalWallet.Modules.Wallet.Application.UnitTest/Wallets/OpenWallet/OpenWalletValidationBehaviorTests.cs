using DigitalWallet.Modules.Wallets.Application.Behaviors;
using DigitalWallet.Modules.Wallets.Application.Results;
using DigitalWallet.Modules.Wallets.Application.Wallets.OpenWallet;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallet.Application.UnitTest.Wallets.OpenWallet
{
    public sealed class OpenWalletValidationBehaviorTests
    {
        [Fact]
        public async Task Handle_InvalidCommand_ReturnsValidationFailure()
        {
            var validators =
                new IValidator<OpenWalletCommand>[]
                {
                new OpenWalletCommandValidator()
                };

            var behavior =
                new ValidationBehavior<
                    OpenWalletCommand,
                    Result<OpenWalletResponse>>(validators);

            var command =
                new OpenWalletCommand(
                    Guid.Empty,
                    "12");

            var nextWasCalled = false;

            Task<Result<OpenWalletResponse>> Next(CancellationToken cancellationToken)
            {
                nextWasCalled = true;

                return Task.FromResult(
                    Result<OpenWalletResponse>.Success(
                        new OpenWalletResponse(
                            Guid.NewGuid(),
                            Guid.NewGuid(),
                            "AUD",
                            "PendingActivation")));
            }

            var result = await behavior.Handle(command, new RequestHandlerDelegate<Result<OpenWalletResponse>>(Next), CancellationToken.None);

            Assert.True(result.IsFailure);
            Assert.False(nextWasCalled);
            Assert.Contains(
                result.Errors,
                error => error.Code == "wallet.customer_id_required");
        }
    }
}
