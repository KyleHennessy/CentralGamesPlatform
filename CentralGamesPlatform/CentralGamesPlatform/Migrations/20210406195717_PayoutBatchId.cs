using Microsoft.EntityFrameworkCore.Migrations;

namespace CentralGamesPlatform.Migrations
{
    public partial class PayoutBatchId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PayPalBatchId",
                table: "Payouts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayPalBatchId",
                table: "Payouts");
        }
    }
}
