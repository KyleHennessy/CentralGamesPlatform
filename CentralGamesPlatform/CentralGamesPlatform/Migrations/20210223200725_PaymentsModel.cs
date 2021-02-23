using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CentralGamesPlatform.Migrations
{
    public partial class PaymentsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(nullable: false),
                    StripeSession = table.Column<string>(nullable: true),
                    StripeEmail = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    Total = table.Column<decimal>(nullable: false),
                    PaymentDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");
        }
    }
}
