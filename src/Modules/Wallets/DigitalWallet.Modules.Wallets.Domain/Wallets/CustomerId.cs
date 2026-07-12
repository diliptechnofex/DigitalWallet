using DigitalWallet.BuildingBlocks.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Domain.Wallets
{
    public readonly record struct CustomerId
    {
        private CustomerId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public bool IsEmpty => Value == Guid.Empty;

        public static CustomerId From(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new DomainRuleViolationException(
                    "wallet.invalid_customer_id",
                    "Customer identifier cannot be empty.");
            }

            return new CustomerId(value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
