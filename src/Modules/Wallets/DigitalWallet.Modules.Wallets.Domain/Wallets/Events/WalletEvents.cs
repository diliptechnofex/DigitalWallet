using DigitalWallet.BuildingBlocks.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Domain.Wallets.Events
{
    public sealed record WalletOpenedDomainEvent(Guid EventId,
                                                 WalletId WalletId,
                                                 CustomerId CustomerId,
                                                 CurrencyCode Currency,
                                                 DateTimeOffset OccurredAtUtc)
                                                 : IDomainEvent;

    public sealed record WalletActivatedDomainEvent(
    Guid EventId,
    WalletId WalletId,
    DateTimeOffset OccurredAtUtc)
    : IDomainEvent;


    public sealed record WalletSuspendedDomainEvent(
    Guid EventId,
    WalletId WalletId,
    WalletSuspensionReason Reason,
    DateTimeOffset OccurredAtUtc)
    : IDomainEvent;

    public sealed record WalletResumedDomainEvent(
    Guid EventId,
    WalletId WalletId,
    DateTimeOffset OccurredAtUtc)
    : IDomainEvent;

    public sealed record WalletClosedDomainEvent(
    Guid EventId,
    WalletId WalletId,
    DateTimeOffset OccurredAtUtc)
    : IDomainEvent;
}
