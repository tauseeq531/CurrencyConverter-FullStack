using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyConverter.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToConversions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Conversions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Conversions");
        }
    }
}
