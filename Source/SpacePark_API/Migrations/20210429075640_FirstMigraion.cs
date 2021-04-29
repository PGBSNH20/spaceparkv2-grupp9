using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpacePark_API.Migrations
{
    public partial class FirstMigraion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parking",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpaceShip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Payed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parking", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayID = table.Column<int>(type: "int", nullable: false),
                    PersonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpaceShip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pay",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParkID = table.Column<int>(type: "int", nullable: false),
                    Invoice = table.Column<int>(type: "int", nullable: false),
                    DepartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pay", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pay_Parking_ParkID",
                        column: x => x.ParkID,
                        principalTable: "Parking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pay_ParkID",
                table: "Pay",
                column: "ParkID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pay");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "Parking");
        }
    }
}
