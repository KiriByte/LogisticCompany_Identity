using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticCompany_Identity.Migrations
{
    /// <inheritdoc />
    public partial class changeCarModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Available",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "DriverName",
                table: "Cars");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "Cars",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DriverName",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
