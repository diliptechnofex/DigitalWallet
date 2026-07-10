using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.BuildingBlocks.Domain.Exceptions
{
    public sealed class DomainRuleViolationException : Exception
    {
        public DomainRuleViolationException(
            string code,
            string message)
            : base(message)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(code);

            Code = code;
        }

        public string Code { get; }
    }
}
