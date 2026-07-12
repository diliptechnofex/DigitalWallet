using DigitalWallet.BuildingBlocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Domain.Wallets
{
    public readonly record struct WalletId
    {
        private WalletId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public bool IsEmpty => Value == Guid.Empty;

        public static WalletId New()
        {
            return new WalletId(Guid.NewGuid());
        }

        public static WalletId From(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new DomainRuleViolationException(
                    "wallet.invalid_id",
                    "Wallet identifier cannot be empty.");
            }

            return new WalletId(value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
