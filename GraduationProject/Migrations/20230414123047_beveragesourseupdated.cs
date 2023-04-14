using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    /// <inheritdoc />
    public partial class beveragesourseupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Favorites",
                columns: new[] { "FavoriteId", "BeverageId", "Source", "UserId" },
                values: new object[] { 3, 11000, 1, 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Favorites",
                keyColumn: "FavoriteId",
                keyValue: 3);
        }
    }
}
