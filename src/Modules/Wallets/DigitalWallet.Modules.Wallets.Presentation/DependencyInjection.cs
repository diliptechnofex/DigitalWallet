using DigitalWallet.Modules.Wallets.Presentation.Wallets;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Presentation
{
    public static class DependencyInjection
    {
        public static IEndpointRouteBuilder MapWalletsPresentation(
        this IEndpointRouteBuilder app)
        {
            app.MapWalletsEndpoints();

            return app;
        }
    }
}
