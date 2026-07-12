using DigitalWallet.Modules.Wallets.Application.Repository;
using DigitalWallet.Modules.Wallets.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWalletsInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString =
                configuration.GetConnectionString("WalletsDatabase")
                ?? throw new InvalidOperationException(
                    "Connection string 'WalletsDatabase' was not found.");

            services.AddDbContext<WalletsDbContext>(
                options =>
                {
                    options.UseNpgsql(connectionString,
                        npgSqlOptions =>
                        {
                            npgSqlOptions.MigrationsHistoryTable("__ef_migrations_history", "wallets");
                        });
                });

            services.AddScoped<IWalletRepository, WalletRepository>();

            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<WalletsDbContext>());

            return services;
        }
    }
}
