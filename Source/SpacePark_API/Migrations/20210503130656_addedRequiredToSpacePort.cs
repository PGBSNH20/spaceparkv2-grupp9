using Microsoft.EntityFrameworkCore.Migrations;

namespace SpacePark_API.Migrations
{
    public partial class addedRequiredToSpacePort : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parking_SpacePorts_SpacePortID",
                table: "Parking");

            migrationBuilder.AlterColumn<int>(
                name: "SpacePortID",
                table: "Parking",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Parking_SpacePorts_SpacePortID",
                table: "Parking",
                column: "SpacePortID",
                principalTable: "SpacePorts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parking_SpacePorts_SpacePortID",
                table: "Parking");

            migrationBuilder.AlterColumn<int>(
                name: "SpacePortID",
                table: "Parking",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Parking_SpacePorts_SpacePortID",
                table: "Parking",
                column: "SpacePortID",
                principalTable: "SpacePorts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
