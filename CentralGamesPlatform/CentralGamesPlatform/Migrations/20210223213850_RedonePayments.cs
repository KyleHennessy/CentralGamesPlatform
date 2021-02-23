using Microsoft.EntityFrameworkCore.Migrations;

namespace CentralGamesPlatform.Migrations
{
    public partial class RedonePayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "StripeEmail",
                table: "Payments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StripeEmail",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
