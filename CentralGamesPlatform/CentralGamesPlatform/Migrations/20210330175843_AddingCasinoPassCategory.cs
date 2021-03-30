using Microsoft.EntityFrameworkCore.Migrations;

namespace CentralGamesPlatform.Migrations
{
    public partial class AddingCasinoPassCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryDescription", "CategoryName" },
                values: new object[] { -1, null, "Casino Pass" });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "GameId", "CategoryId", "Description", "ImageThumbnailUrl", "ImageUrl", "IsOnSale", "Name", "Price" },
                values: new object[] { -1, -1, "Used to play casino games. One time use", null, null, false, "Casino Pass", 5.00m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: -1);
        }
    }
}
