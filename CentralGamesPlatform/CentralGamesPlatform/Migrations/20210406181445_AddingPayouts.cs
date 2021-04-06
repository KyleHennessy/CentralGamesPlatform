using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CentralGamesPlatform.Migrations
{
    public partial class AddingPayouts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payouts",
                columns: table => new
                {
                    PayoutId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WalletId = table.Column<int>(nullable: false),
                    AmountTransfered = table.Column<decimal>(nullable: false),
                    PayPalEmail = table.Column<string>(nullable: false),
                    PayoutDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payouts", x => x.PayoutId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payouts");
        }
    }
}
