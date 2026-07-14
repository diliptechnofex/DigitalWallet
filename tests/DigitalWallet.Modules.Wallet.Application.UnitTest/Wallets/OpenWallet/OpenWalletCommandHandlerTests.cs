using DigitalWallet.Modules.Wallets.Application.Repository;
using DigitalWallet.Modules.Wallets.Application.Wallets.OpenWallet;
using DigitalWallet.Modules.Wallets.Domain.Wallets;
using DigitalWallet.Modules.Wallets.Domain.Wallets.Aggregate;

namespace DigitalWallet.Modules.Wallets.Application.UnitTests.Wallets.OpenWallet;

public sealed class OpenWalletCommandHandlerTests
{
//    [Fact]
//    public async Task Handle_ValidCommand_CreatesWallet()
//    {
//        var repository = new FakeWalletRepository();
//        var unitOfWork = new FakeUnitOfWork();

//        var handler = new OpenWalletCommandHandler(
//            repository,
//            unitOfWork,
//            TimeProvider.System);

//        var command = new OpenWalletCommand(Guid.NewGuid(), "AUD");

//        var result = await handler.Handle(command, CancellationToken.None);

//        Assert.True(result.IsSuccess);
//        Assert.NotEqual(Guid.Empty, result.Value.WalletId);
//        Assert.Equal("AUD", result.Value.Currency);
//        Assert.Equal("PendingActivation", result.Value.Status);
//        Assert.NotNull(repository.AddedWallet);
//        Assert.Equal(1, unitOfWork.SaveChangesCount);
//    }

//    private sealed class FakeWalletRepository : IWalletRepository
//    {
//        public bool Exists { get; init; }

//        public Wallet? AddedWallet { get; private set; }

//        public Task<Wallet?> GetByIdAsync(
//            WalletId walletId,
//            CancellationToken cancellationToken)
//        {
//            return Task.FromResult<Wallet?>(null);
//        }

//        public Task<bool> ExistsForCustomerAndCurrencyAsync(
//            CustomerId customerId,
//            CurrencyCode currency,
//            CancellationToken cancellationToken)
//        {
//            return Task.FromResult(Exists);
//        }

//        public void Add(Wallet wallet)
//        {
//            AddedWallet = wallet;
//        }
//    }

//    private sealed class FakeUnitOfWork : IUnitOfWork
//    {
//        public int SaveChangesCount { get; private set; }

//        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
//        {
//            SaveChangesCount++;
//            return Task.FromResult(1);
//        }
//    }
}

