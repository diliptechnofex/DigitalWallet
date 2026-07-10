using DigitalWallet.BuildingBlocks.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.BuildingBlocks.Domain.Primitives
{
    public abstract class AggregateRoot<TId> : Entity<TId>
     where TId : notnull
    {
        private readonly List<IDomainEvent> _domainEvents = [];

        protected AggregateRoot()
        {
        }

        protected AggregateRoot(TId id)
            : base(id)
        {
        }

        public IReadOnlyCollection<IDomainEvent> DomainEvents =>
            _domainEvents;

        protected void RaiseDomainEvent(
            IDomainEvent domainEvent)
        {
            ArgumentNullException.ThrowIfNull(domainEvent);

            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
