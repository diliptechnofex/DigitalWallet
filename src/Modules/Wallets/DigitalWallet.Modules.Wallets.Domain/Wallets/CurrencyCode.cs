using DigitalWallet.BuildingBlocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Domain.Wallets
{
    public sealed record CurrencyCode
    {
        private CurrencyCode(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static CurrencyCode Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainRuleViolationException(
                    "wallet.currency_required",
                    "Wallet currency is required.");
            }

            var normalized = value.Trim().ToUpperInvariant();

            if (normalized.Length != 3 ||
                !normalized.All(char.IsLetter))
            {
                throw new DomainRuleViolationException(
                    "wallet.invalid_currency",
                    "Currency must contain exactly three letters.");
            }

            return new CurrencyCode(normalized);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
