using DigitalWallet.Modules.Wallets.Domain.Wallets;
using DigitalWallet.Modules.Wallets.Domain.Wallets.Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Infrastructure.Persistence.Configurations
{
    internal sealed class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.ToTable(
                "wallets",
                "wallets",
                tableBuilder =>
                {
                    tableBuilder.HasCheckConstraint(
                        "ck_wallets_currency_length",
                        "length(currency) = 3");

                    tableBuilder.HasCheckConstraint(
                        "ck_wallets_status_valid",
                        "status in ('PendingActivation', 'Active', 'Suspended', 'Closed')");
                });

            builder.HasKey(wallet => wallet.Id);

            builder.Property(wallet => wallet.Id)
                .HasColumnName("id")
                .HasConversion(
                    walletId => walletId.Value,
                    value => WalletId.From(value))
                .ValueGeneratedNever();

            builder.Property(wallet => wallet.CustomerId)
                .HasColumnName("customer_id")
                .HasConversion(
                    customerId => customerId.Value,
                    value => CustomerId.From(value))
                .IsRequired();

            builder.Property(wallet => wallet.Currency)
                .HasColumnName("currency")
                .HasConversion(
                    currency => currency.Value,
                    value => CurrencyCode.Create(value))
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(wallet => wallet.Status)
                .HasColumnName("status")
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(wallet => wallet.SuspensionReason)
                .HasColumnName("suspension_reason")
                .HasConversion(
                    reason => reason == null ? null : reason.Value,
                    value => value == null
                        ? null
                        : WalletSuspensionReason.Create(value))
                .HasMaxLength(250);

            builder.Property(wallet => wallet.OpenedAtUtc)
                .HasColumnName("opened_at_utc")
                .IsRequired();

            builder.Property(wallet => wallet.ActivatedAtUtc)
                .HasColumnName("activated_at_utc");

            builder.Property(wallet => wallet.SuspendedAtUtc)
                .HasColumnName("suspended_at_utc");

            builder.Property(wallet => wallet.ClosedAtUtc)
                .HasColumnName("closed_at_utc");

            builder.Property(wallet => wallet.Version)
                .IsRowVersion();

            builder.HasIndex(
                    wallet => new
                    {
                        wallet.CustomerId,
                        wallet.Currency
                    })
                .IsUnique()
                .HasDatabaseName(
                    "ux_wallets_customer_currency");
        }
    }
}
