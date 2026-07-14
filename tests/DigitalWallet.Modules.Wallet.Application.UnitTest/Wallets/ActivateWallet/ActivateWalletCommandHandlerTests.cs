using DigitalWallet.Modules.Wallets.Application.Repository;
using DigitalWallet.Modules.Wallets.Application.Wallets.ActivateWallet;
using DigitalWallet.Modules.Wallets.Domain.Wallets;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Application.UnitTest.Wallets.ActivateWallet
{
    public sealed class ActivateWalletCommandHandlerTests
    {
        //[Fact]
        //public async Task Handle_PendingWallet_ActivatesWallet()
        //{
        //    var wallet = Wallet.Open(
        //        CustomerId.From(Guid.NewGuid()),
        //        CurrencyCode.Create("AUD"),
        //        DateTimeOffset.UtcNow);

        //    var repository = new FakeWalletRepository(wallet);
        //    var unitOfWork = new FakeUnitOfWork();

        //    var handler = CreateHandler(
        //        repository,
        //        unitOfWork);

        //    var result = await handler.Handle(
        //        new ActivateWalletCommand(wallet.Id.Value),
        //        CancellationToken.None);

        //    Assert.True(result.IsSuccess);
        //    Assert.Equal("Active", result.Value.Status);
        //    Assert.Equal(WalletStatus.Active, wallet.Status);
        //    Assert.NotNull(wallet.ActivatedAtUtc);
        //    Assert.Equal(1, unitOfWork.SaveChangesCount);
        //}

        //[Fact]
        //public async Task Handle_WalletDoesNotExist_ReturnsNotFound()
        //{
        //    var repository = new FakeWalletRepository(null);
        //    var unitOfWork = new FakeUnitOfWork();

        //    var handler = CreateHandler(
        //        repository,
        //        unitOfWork);

        //    var result = await handler.Handle(
        //        new ActivateWalletCommand(Guid.NewGuid()),
        //        CancellationToken.None);

        //    Assert.True(result.IsFailure);
        //    Assert.Contains(
        //        result.Errors,
        //        error => error.Code == "wallet.not_found");

        //    Assert.Equal(0, unitOfWork.SaveChangesCount);
        //}

        //[Fact]
        //public async Task Handle_AlreadyActiveWallet_ReturnsInvalidStateTransition()
        //{
        //    var wallet = Wallet.Open(
        //        CustomerId.From(Guid.NewGuid()),
        //        CurrencyCode.Create("AUD"),
        //        DateTimeOffset.UtcNow);

        //    wallet.Activate(DateTimeOffset.UtcNow);

        //    var repository = new FakeWalletRepository(wallet);
        //    var unitOfWork = new FakeUnitOfWork();

        //    var handler = CreateHandler(
        //        repository,
        //        unitOfWork);

        //    var result = await handler.Handle(
        //        new ActivateWalletCommand(wallet.Id.Value),
        //        CancellationToken.None);

        //    Assert.True(result.IsFailure);
        //    Assert.Contains(
        //        result.Errors,
        //        error => error.Code == "wallet.invalid_state_transition");

        //    Assert.Equal(0, unitOfWork.SaveChangesCount);
        //}

        //[Fact]
        //public async Task Handle_ClosedWallet_ReturnsInvalidStateTransition()
        //{
        //    var wallet = Wallet.Open(
        //        CustomerId.From(Guid.NewGuid()),
        //        CurrencyCode.Create("AUD"),
        //        DateTimeOffset.UtcNow);

        //    wallet.Close(DateTimeOffset.UtcNow);

        //    var repository = new FakeWalletRepository(wallet);
        //    var unitOfWork = new FakeUnitOfWork();

        //    var handler = CreateHandler(
        //        repository,
        //        unitOfWork);

        //    var result = await handler.Handle(
        //        new ActivateWalletCommand(wallet.Id.Value),
        //        CancellationToken.None);

        //    Assert.True(result.IsFailure);
        //    Assert.Contains(
        //        result.Errors,
        //        error => error.Code == "wallet.invalid_state_transition");

        //    Assert.Equal(0, unitOfWork.SaveChangesCount);
        //}

        //private static ActivateWalletCommandHandler CreateHandler(
        //    IWalletRepository repository,
        //    IUnitOfWork unitOfWork)
        //{
        //    return new ActivateWalletCommandHandler(
        //        repository,
        //        unitOfWork,
        //        TimeProvider.System);
        //}

        //private sealed class FakeWalletRepository(
        //    Wallet? wallet)
        //    : IWalletRepository
        //{
        //    public Task<Wallet?> GetByIdAsync(
        //        WalletId walletId,
        //        CancellationToken cancellationToken)
        //    {
        //        return Task.FromResult(wallet);
        //    }

        //    public Task<bool> ExistsForCustomerAndCurrencyAsync(
        //        CustomerId customerId,
        //        CurrencyCode currency,
        //        CancellationToken cancellationToken)
        //    {
        //        return Task.FromResult(false);
        //    }

        //    public void Add(Wallet wallet)
        //    {
        //    }
        //}

        private sealed class FakeUnitOfWork : IUnitOfWork
        {
            public int SaveChangesCount { get; private set; }

            public Task<int> SaveChangesAsync(
                CancellationToken cancellationToken)
            {
                SaveChangesCount++;

                return Task.FromResult(1);
            }
        }
    }
}
