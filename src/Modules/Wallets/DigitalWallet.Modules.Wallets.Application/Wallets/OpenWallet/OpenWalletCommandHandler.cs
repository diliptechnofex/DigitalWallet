using DigitalWallet.Modules.Wallets.Application.Repository;
using DigitalWallet.Modules.Wallets.Application.Results;
using DigitalWallet.Modules.Wallets.Domain.Wallets;
using DigitalWallet.Modules.Wallets.Domain.Wallets.Aggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Application.Wallets.OpenWallet
{
    public sealed class OpenWalletCommandHandler(IWalletRepository walletRepository, IUnitOfWork unitOfWork, TimeProvider timeProvider) : IRequestHandler<OpenWalletCommand, Result<OpenWalletResponse>>
    {
        public async Task<Result<OpenWalletResponse>> Handle(OpenWalletCommand request, CancellationToken cancellationToken)
        {
            var customerId = CustomerId.From(request.CustomerId);

            var currency = CurrencyCode.Create(request.Currency);

            var alreadyExists = await walletRepository.ExistsForCustomerAndCurrencyAsync(customerId, currency, cancellationToken);

            if (alreadyExists)
            {
                return Result<OpenWalletResponse>.Failure(WalletErrors.AlreadyExists(currency.Value));
            }

            var wallet = Wallet.Open(customerId, currency, timeProvider.GetUtcNow());

            walletRepository.Add(wallet);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            var response = new OpenWalletResponse(
                WalletId: wallet.Id.Value,
                CustomerId: wallet.CustomerId.Value,
                Currency: wallet.Currency.Value,
                Status: wallet.Status.ToString());

            return Result<OpenWalletResponse>.Success(response);
        }
    }
}
