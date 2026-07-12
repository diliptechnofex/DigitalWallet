using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalWallet.Modules.Wallets.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialWalletsSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "wallets");

            migrationBuilder.CreateTable(
                name: "wallets",
                schema: "wallets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    status = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    suspension_reason = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    opened_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    activated_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    suspended_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    closed_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wallets", x => x.id);
                    table.CheckConstraint("ck_wallets_currency_length", "length(currency) = 3");
                    table.CheckConstraint("ck_wallets_status_valid", "status in ('PendingActivation', 'Active', 'Suspended', 'Closed')");
                });

            migrationBuilder.CreateIndex(
                name: "ux_wallets_customer_currency",
                schema: "wallets",
                table: "wallets",
                columns: new[] { "customer_id", "currency" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wallets",
                schema: "wallets");
        }
    }
}
