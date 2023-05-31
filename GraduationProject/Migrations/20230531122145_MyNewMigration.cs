using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GraduationProject.Migrations
{
    /// <inheritdoc />
    public partial class MyNewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Beverages",
                columns: table => new
                {
                    BeverageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Alcohol = table.Column<bool>(type: "bit", nullable: false),
                    Glass = table.Column<int>(type: "int", nullable: false),
                    Instruction = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageAttribution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreativeCommonsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Source = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beverages", x => x.BeverageId);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.IngredientId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "BeverageIngredients",
                columns: table => new
                {
                    BeverageIngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BeverageId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    Measurement = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeverageIngredients", x => x.BeverageIngredientId);
                    table.ForeignKey(
                        name: "FK_BeverageIngredients_Beverages_BeverageId",
                        column: x => x.BeverageId,
                        principalTable: "Beverages",
                        principalColumn: "BeverageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeverageIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    FavoriteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FavoriteBeverageId = table.Column<int>(type: "int", nullable: false),
                    Source = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.FavoriteId);
                    table.ForeignKey(
                        name: "FK_Favorites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Beverages",
                columns: new[] { "BeverageId", "Alcohol", "ApiId", "CreativeCommonsConfirmed", "Glass", "Image", "ImageAttribution", "Instruction", "Name", "Source", "Tag", "Video" },
                values: new object[,]
                {
                    { 1, true, 0, false, 29, "http://potatomargarita.com", null, "Shake it like a polaroid picture", "Potato Margarita", 0, "ordinary", null },
                    { 2, true, 0, false, 27, "http://tomatomartini.com", null, "Stir it up", "Tomato Martini", 0, "cocktail", null },
                    { 3, false, 0, false, 31, "http://brocolioldfashined.com", null, "On the grind", "Brocoli Old Fashioned", 0, "ordinary", null }
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
                columns: new[] { "UserId", "UserName" },
                values: new object[,]
                {
                    { 1, "ChuckNorris" },
                    { 2, "BruceLee" }
                });

            migrationBuilder.InsertData(
                table: "BeverageIngredients",
                columns: new[] { "BeverageIngredientId", "BeverageId", "IngredientId", "Measurement" },
                values: new object[,]
                {
                    { 1, 1, 1, "60ml" },
                    { 2, 1, 2, "One Slice" },
                    { 3, 1, 3, "35ml" },
                    { 4, 2, 2, "One Slice" },
                    { 5, 2, 3, "35ml" }
                });

            migrationBuilder.InsertData(
                table: "Favorites",
                columns: new[] { "FavoriteId", "FavoriteBeverageId", "Source", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 0, 1 },
                    { 2, 2, 0, 2 },
                    { 3, 11000, 1, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeverageIngredients_BeverageId",
                table: "BeverageIngredients",
                column: "BeverageId");

            migrationBuilder.CreateIndex(
                name: "IX_BeverageIngredients_IngredientId",
                table: "BeverageIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeverageIngredients");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Beverages");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
