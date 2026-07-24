using DigitalWallet.BuildingBlocks.Domain.Exceptions;
using DigitalWallet.BuildingBlocks.Domain.Primitives;
using DigitalWallet.Modules.Wallets.Domain.Wallets.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Domain.Wallets.Aggregate
{
    public sealed class Wallet : AggregateRoot<WalletId>
    {
        private Wallet()
        {
        }

        private Wallet(
            WalletId id,
            CustomerId customerId,
            CurrencyCode currency,
            DateTimeOffset openedAtUtc)
            : base(id)
        {
            CustomerId = customerId;
            Currency = currency;
            Status = WalletStatus.PendingActivation;
            OpenedAtUtc = openedAtUtc;

            RaiseDomainEvent(
                new WalletOpenedDomainEvent(
                    EventId: Guid.NewGuid(),
                    WalletId: Id,
                    CustomerId: CustomerId,
                    Currency: Currency,
                    OccurredAtUtc: openedAtUtc));
        }
      

        public CustomerId CustomerId { get; private set; }

        public CurrencyCode Currency { get; private set; } = null!;

        public WalletStatus Status { get; private set; }

        public WalletSuspensionReason? SuspensionReason
        {
            get;
            private set;
        }

        public DateTimeOffset OpenedAtUtc { get; private set; }

        public DateTimeOffset? ActivatedAtUtc { get; private set; }

        public DateTimeOffset? SuspendedAtUtc { get; private set; }

        public uint Version { get; private set; }

        public DateTimeOffset? ClosedAtUtc { get; private set; }

        public static Wallet Open(
            CustomerId customerId,
            CurrencyCode currency,
            DateTimeOffset occurredAtUtc)
        {
            if (customerId.IsEmpty)
            {
                throw new DomainRuleViolationException(
                    "wallet.invalid_customer_id",
                    "Customer identifier cannot be empty.");
            }

            ArgumentNullException.ThrowIfNull(currency);

            var utcTimestamp = NormalizeUtc(occurredAtUtc);

            return new Wallet(
                WalletId.New(),
                customerId,
                currency,
                utcTimestamp);
        }

        public void Activate(DateTimeOffset occurredAtUtc)
        {
            EnsureStatus(WalletStatus.PendingActivation,"Only a pending wallet can be activated.");

            var utcTimestamp = NormalizeUtc(occurredAtUtc);

            Status = WalletStatus.Active;
            ActivatedAtUtc = utcTimestamp;

            RaiseDomainEvent(
                new WalletActivatedDomainEvent(
                    EventId: Guid.NewGuid(),
                    WalletId: Id,
                    OccurredAtUtc: utcTimestamp));
        }

        public void Suspend(WalletSuspensionReason reason, DateTimeOffset occurredAtUtc)
        {
            EnsureStatus(WalletStatus.Active, "Only an active wallet can be suspended.");

            ArgumentNullException.ThrowIfNull(reason);

            var utcTimestamp = NormalizeUtc(occurredAtUtc);

            Status = WalletStatus.Suspended;
            SuspensionReason = reason;
            SuspendedAtUtc = utcTimestamp;

            RaiseDomainEvent(
                new WalletSuspendedDomainEvent(
                    EventId: Guid.NewGuid(),
                    WalletId: Id,
                    Reason: reason,
                    OccurredAtUtc: utcTimestamp));
        }

        public void Resume(DateTimeOffset occurredAtUtc)
        {
            EnsureStatus(WalletStatus.Suspended, "Only a suspended wallet can be resumed.");

            var utcTimestamp = NormalizeUtc(occurredAtUtc);

            Status = WalletStatus.Active;
            SuspensionReason = null;
            SuspendedAtUtc = null;

            RaiseDomainEvent(new WalletResumedDomainEvent(
                             EventId: Guid.NewGuid(),
                             WalletId: Id,
                             OccurredAtUtc: utcTimestamp));
        }

        public void Close(DateTimeOffset occurredAtUtc)
        {
            if (Status == WalletStatus.Closed)
            {
                throw new DomainRuleViolationException(
                    "wallet.already_closed",
                    "The wallet is already closed.");
            }

            var utcTimestamp = NormalizeUtc(occurredAtUtc);

            Status = WalletStatus.Closed;
            SuspensionReason = null;
            ClosedAtUtc = utcTimestamp;

            RaiseDomainEvent(
                new WalletClosedDomainEvent(
                    EventId: Guid.NewGuid(),
                    WalletId: Id,
                    OccurredAtUtc: utcTimestamp));
        }

        private void EnsureStatus(WalletStatus requiredStatus, string message)
        {
            if (Status == requiredStatus)
            {
                return;
            }

            throw new DomainRuleViolationException("wallet.invalid_status_transition", $"{message} Current status is {Status}.");
        }

        private static DateTimeOffset NormalizeUtc(DateTimeOffset value)
        {
            return value.ToUniversalTime();
        }
    }
}
