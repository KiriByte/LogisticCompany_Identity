using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticCompany_Identity.Migrations
{
    /// <inheritdoc />
    public partial class addFreightRequestModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FreightRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartureCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Approved = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreightRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FreightRequests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FreightRequests_UserId",
                table: "FreightRequests",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FreightRequests");
        }
    }
}
