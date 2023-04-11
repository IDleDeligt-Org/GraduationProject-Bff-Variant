using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GraduationProject.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Beverages",
                columns: new[] { "BeverageId", "Alcohol", "Glass", "Image", "Instruction", "Name", "Tag", "Video" },
                values: new object[,]
                {
                    { 1, true, "Martini Glass", "http://potatomargarita.com", "Shake it like a polaroid picture", "Potato Margarita", "ordinary", null },
                    { 2, true, "Thumbler", "http://tomatomartini.com", "Stir it up", "Tomato Martini", "cocktail", null },
                    { 3, false, "Long glass", "http://brocolioldfashined.com", "On the grind", "Brocoli Old Fashioned", "ordinary", null }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "IngredientId", "Description", "Image", "Name" },
                values: new object[,]
                {
                    { 1, "Great vegetable, quite bitter", "http://brocoli.com", "Brocoli Liqueur" },
                    { 2, "Saved nations from famine", "http://potato.com", "Potato" },
                    { 3, "The italian berry", "http://tomato.com", "Tomato extract" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, "kickass@gmail.com", "NinjaKick", "ChuckNorris" },
                    { 2, "iiiiiijjjaaa@hotmail.com", "RoundHouseKick", "BruceLee" }
                });

            migrationBuilder.InsertData(
                table: "BeverageIngredients",
                columns: new[] { "BeverageIngredientId", "BeverageId", "IngredientId", "Measurment" },
                values: new object[,]
                {
                    { 1, 1, 1, "60ml" },
                    { 2, 1, 2, "One Slice" },
                    { 3, 1, 3, "35ml" }
                });

            migrationBuilder.InsertData(
                table: "Favorites",
                columns: new[] { "FavoriteId", "BeverageId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BeverageIngredients",
                keyColumn: "BeverageIngredientId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BeverageIngredients",
                keyColumn: "BeverageIngredientId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BeverageIngredients",
                keyColumn: "BeverageIngredientId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Beverages",
                keyColumn: "BeverageId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Favorites",
                keyColumn: "FavoriteId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Favorites",
                keyColumn: "FavoriteId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Beverages",
                keyColumn: "BeverageId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Beverages",
                keyColumn: "BeverageId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "IngredientId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "IngredientId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "IngredientId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);
        }
    }
}
