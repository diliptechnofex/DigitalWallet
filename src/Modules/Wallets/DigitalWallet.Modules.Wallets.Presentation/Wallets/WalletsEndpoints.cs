using DigitalWallet.Modules.Wallets.Application.Wallets.ActivateWallet;
using DigitalWallet.Modules.Wallets.Application.Wallets.OpenWallet;
using DigitalWallet.Modules.Wallets.Presentation.Wallets.Contracts;
using DigitalWallet.Modules.Wallets.Presentation.Wallets.Contracts.ActivateWallet;
using DigitalWallet.Modules.Wallets.Presentation.Wallets.Contracts.OpenWallet;
using DigitalWallet.Modules.Wallets.Presentation.Wallets.MapResults;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Presentation.Wallets
{
    public static class WalletsEndpoints
    {
        public static IEndpointRouteBuilder MapWalletsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/wallets").WithTags("Wallets");

            group.MapPost("/", OpenWalletAsync)
                 .WithName("OpenWallet")
                 .WithSummary("Open a wallet")
                 .WithDescription(
                     "Creates a new wallet for a customer in a specific currency.")
                 .Produces<OpenWalletApiResponse>(
                     StatusCodes.Status201Created)
                 .ProducesProblem(
                     StatusCodes.Status400BadRequest)
                 .ProducesProblem(
                     StatusCodes.Status409Conflict)
                 .ProducesProblem(
                     StatusCodes.Status500InternalServerError);


            group.MapPost("/{walletId:guid}/activate", ActivateWalletAsync)
                .WithName("ActivateWallet")
                .WithSummary("Activate a wallet")
                .WithDescription("Active wallet for a customer in a specific currency.")
                .Produces<ActivateWalletApiResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status409Conflict)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            return app;
        }

        private static async Task<IResult> OpenWalletAsync(OpenWalletRequest request,
                                                           ISender sender,
                                                           HttpContext httpContext,
                                                           CancellationToken cancellationToken)
        {
            var command = new OpenWalletCommand(request.CustomerId, request.Currency);

            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return result.ToProblem(httpContext);
            }

            var response = new OpenWalletApiResponse(WalletId: result.Value.WalletId,
                                                     CustomerId: result.Value.CustomerId,
                                                     Currency: result.Value.Currency,
                                                     Status: result.Value.Status);

            return Results.Created($"/api/wallets/{response.WalletId}", response);
        }

        private static async Task<IResult> ActivateWalletAsync(Guid walletId,
                                                             ISender sender,
                                                             HttpContext httpContext,
                                                             CancellationToken cancellationToken)
        {
            var command = new ActivateWalletCommand(walletId);

            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return result.ToProblem(httpContext);
            }

            var response = new ActivateWalletApiResponse(WalletId: result.Value.WalletId, Status: result.Value.Status);

            return Results.Ok(response);
        }
    }
}
