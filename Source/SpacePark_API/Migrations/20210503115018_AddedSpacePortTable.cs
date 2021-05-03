using Microsoft.EntityFrameworkCore.Migrations;

namespace SpacePark_API.Migrations
{
    public partial class AddedSpacePortTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpacePortID",
                table: "Parking",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SpacePorts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalCapacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpacePorts", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parking_SpacePortID",
                table: "Parking",
                column: "SpacePortID");

            migrationBuilder.AddForeignKey(
                name: "FK_Parking_SpacePorts_SpacePortID",
                table: "Parking",
                column: "SpacePortID",
                principalTable: "SpacePorts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parking_SpacePorts_SpacePortID",
                table: "Parking");

            migrationBuilder.DropTable(
                name: "SpacePorts");

            migrationBuilder.DropIndex(
                name: "IX_Parking_SpacePortID",
                table: "Parking");

            migrationBuilder.DropColumn(
                name: "SpacePortID",
                table: "Parking");
        }
    }
}
