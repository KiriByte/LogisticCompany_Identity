using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticCompany_Identity.Migrations
{
    /// <inheritdoc />
    public partial class addAvailableCarStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "Cars",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Available",
                table: "Cars");
        }
    }
}
