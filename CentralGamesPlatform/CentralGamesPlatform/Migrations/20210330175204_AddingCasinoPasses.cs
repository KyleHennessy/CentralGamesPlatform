using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CentralGamesPlatform.Migrations
{
    public partial class AddingCasinoPasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CasinoPass",
                table: "OrderDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CasinoPasses",
                columns: table => new
                {
                    CasinoPassId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    PassPlaced = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    GameId = table.Column<int>(nullable: false),
                    Expired = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CasinoPasses", x => x.CasinoPassId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CasinoPasses");

            migrationBuilder.DropColumn(
                name: "CasinoPass",
                table: "OrderDetails");
        }
    }
}
