using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticCompany_Identity.Migrations
{
    /// <inheritdoc />
    public partial class addWaybills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Waybills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    FreightRequestId = table.Column<int>(type: "int", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waybills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Waybills_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Waybills_FreightRequests_FreightRequestId",
                        column: x => x.FreightRequestId,
                        principalTable: "FreightRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Waybills_CarId",
                table: "Waybills",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Waybills_FreightRequestId",
                table: "Waybills",
                column: "FreightRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Waybills");
        }
    }
}
