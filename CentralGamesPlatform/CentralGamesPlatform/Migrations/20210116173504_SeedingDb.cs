using Microsoft.EntityFrameworkCore.Migrations;

namespace CentralGamesPlatform.Migrations
{
    public partial class SeedingDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryDescription", "CategoryName" },
                values: new object[,]
                {
                    { 1, null, "Casino Game PC" },
                    { 2, null, "Casino Game Mobile" },
                    { 3, null, "Casino Game Both Device" },
                    { 4, null, "Browser Game PC" },
                    { 5, null, "Browser Game Mobile" },
                    { 6, null, "Browser Game Both Device" },
                    { 7, null, "Download Game PC" },
                    { 8, null, "Download Game Mobile" },
                    { 9, null, "Download Game Both Device" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "GameId", "CategoryId", "Description", "ImageThumbnailUrl", "ImageUrl", "IsOnSale", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "The card game poker where you can potentially win money", "~\\images\\gamethumbnails\\poker.jpg", "~\\images\\gameimages\\pokerLarge.jpg", true, "Poker", 5.00m },
                    { 2, 2, "Spin the wheel for the chance of winning awesome prizes", "~\\images\\gamethumbnails\\placeholder.gif", "~\\images\\gameimages\\placeholder.jpg", true, "Spin the wheel", 5.00m },
                    { 3, 3, "Scratch card where any prizes you reveal are yours to keep", "~\\images\\gamethumbnails\\placeholder.gif", "~\\images\\gameimages\\placeholder.jpg", true, "Scratch card", 5.00m },
                    { 4, 4, "Ride a bike through obstacles to get to the end", "~\\images\\gamethumbnails\\placeholder.gif", "~\\images\\gameimages\\placeholder.jpg", true, "Happy Wheels", 10.00m },
                    { 9, 4, "Defeat all enemies as the imposter to win", "~\\images\\gamethumbnails\\placeholder.gif", "~\\images\\gameimages\\placeholder.jpg", true, "Among us", 5.00m },
                    { 5, 5, "Stack blocks neatly to increase your score", "~\\images\\gamethumbnails\\placeholder.gif", "~\\images\\gameimages\\placeholder.jpg", true, "Tetris", 10.00m },
                    { 6, 6, "Play Pool against your friends", "~\\images\\gamethumbnails\\placeholder.gif", "~\\images\\gameimages\\placeholder.jpg", true, "8 Ball Pool", 0.00m },
                    { 7, 7, "Use weapons to defeat your enemies", "~\\images\\gamethumbnails\\placeholder.gif", "~\\images\\gameimages\\placeholder.jpg", true, "Call of duty", 60.00m },
                    { 8, 8, "Slice the falling fruit to increase your score", "~\\images\\gamethumbnails\\placeholder.gif", "~\\images\\gameimages\\placeholder.jpg", false, "Fruit Ninja", 5.00m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 8);
        }
    }
}
