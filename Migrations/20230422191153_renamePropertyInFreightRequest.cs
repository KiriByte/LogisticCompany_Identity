using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticCompany_Identity.Migrations
{
    /// <inheritdoc />
    public partial class renamePropertyInFreightRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Approved",
                table: "FreightRequests",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "FreightRequests",
                newName: "Approved");
        }
    }
}
