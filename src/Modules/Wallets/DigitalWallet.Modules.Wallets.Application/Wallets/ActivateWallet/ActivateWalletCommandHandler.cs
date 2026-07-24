using DigitalWallet.BuildingBlocks.Domain.Exceptions;
using DigitalWallet.Modules.Wallets.Application.Repository;
using DigitalWallet.Modules.Wallets.Application.Results;
using DigitalWallet.Modules.Wallets.Domain.Wallets;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Application.Wallets.ActivateWallet
{
    public sealed class ActivateWalletCommandHandler(
    IWalletRepository walletRepository,
    IUnitOfWork unitOfWork,
    TimeProvider timeProvider)
    : IRequestHandler<ActivateWalletCommand, Result<ActivateWalletResponse>>
    {
        public async Task<Result<ActivateWalletResponse>> Handle(ActivateWalletCommand request, CancellationToken cancellationToken)
        {
            var walletId = WalletId.From(request.WalletId);

            var wallet = await walletRepository.GetByIdAsync(walletId, cancellationToken);

            if (wallet is null)
            {
                return Result<ActivateWalletResponse>.Failure(WalletErrors.NotFound());
            }

            try
            {
                wallet.Activate(timeProvider.GetUtcNow());
            }
            catch (DomainRuleViolationException exception)
            {
                return Result<ActivateWalletResponse>.Failure(WalletErrors.InvalidStateTransition(exception.Message));
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            var response = new ActivateWalletResponse(WalletId: wallet.Id.Value,
                                                      Status: wallet.Status.ToString(),
                                                      ActivatedAtUtc: wallet.ActivatedAtUtc);

            return Result<ActivateWalletResponse>.Success(response);
        }
    }
}
