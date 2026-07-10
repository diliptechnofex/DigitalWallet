using DigitalWallet.BuildingBlocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Domain.Wallets
{
    public sealed record WalletSuspensionReason
    {
        private const int MaximumLength = 250;

        private WalletSuspensionReason(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static WalletSuspensionReason Create(
            string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainRuleViolationException(
                    "wallet.suspension_reason_required",
                    "A suspension reason is required.");
            }

            var normalized = value.Trim();

            if (normalized.Length > MaximumLength)
            {
                throw new DomainRuleViolationException(
                    "wallet.suspension_reason_too_long",
                    $"Suspension reason cannot exceed {MaximumLength} characters.");
            }

            return new WalletSuspensionReason(normalized);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
