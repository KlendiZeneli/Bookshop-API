using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookNookAPI.Migrations
{
    /// <inheritdoc />
    public partial class NewColumnsLang_BestSeller : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Books");

            migrationBuilder.AddColumn<bool>(
                name: "BestSeller",
                table: "Books",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "English");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BestSeller",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Books");
        }
    }
}
