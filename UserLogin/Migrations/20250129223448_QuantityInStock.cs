using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserLogin.Migrations
{
    /// <inheritdoc />
    public partial class QuantityInStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "quantityInStock",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quantityInStock",
                table: "Books");
        }
    }
}
