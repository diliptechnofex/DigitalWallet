using DigitalWallet.Modules.Wallets.Application.Repository;
using DigitalWallet.Modules.Wallets.Domain.Wallets.Aggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Infrastructure.Persistence
{
    public sealed class WalletsDbContext(DbContextOptions<WalletsDbContext> options) : DbContext(options), IUnitOfWork
    {
        public DbSet<Wallet> Wallets => Set<Wallet>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WalletsDbContext).Assembly);
        }
    }
}
