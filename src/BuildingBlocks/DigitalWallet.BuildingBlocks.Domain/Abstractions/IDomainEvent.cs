using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.BuildingBlocks.Domain.Abstractions
{
    public interface IDomainEvent
    {
        Guid EventId { get; }

        DateTimeOffset OccurredAtUtc { get; }
    }
}
