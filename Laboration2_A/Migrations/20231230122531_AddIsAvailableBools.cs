using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laboration2_A.Migrations
{
    /// <inheritdoc />
    public partial class AddIsAvailableBools : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLoanOpen",
                table: "Loans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLoanOpen",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Books");
        }
    }
}
