using DigitalWallet.Modules.Wallets.Application.Wallets.ActivateWallet;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Application.UnitTest.Wallets.ActivateWallet
{
    public sealed class ActivateWalletCommandValidatorTests
    {
        [Fact]
        public void Validate_ValidCommand_IsValid()
        {
            var validator = new ActivateWalletCommandValidator();

            var result = validator.Validate(
                new ActivateWalletCommand(Guid.NewGuid()));

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Validate_EmptyWalletId_IsInvalid()
        {
            var validator = new ActivateWalletCommandValidator();

            var result = validator.Validate(
                new ActivateWalletCommand(Guid.Empty));

            Assert.False(result.IsValid);
            Assert.Contains(
                result.Errors,
                error => error.ErrorCode == "wallet.wallet_id_required");
        }
    }
}
