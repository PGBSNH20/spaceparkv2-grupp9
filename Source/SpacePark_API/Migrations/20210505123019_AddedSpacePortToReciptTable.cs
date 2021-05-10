using Microsoft.EntityFrameworkCore.Migrations;

namespace SpacePark_API.Migrations
{
    public partial class AddedSpacePortToReciptTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpaceportID",
                table: "Receipts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_SpaceportID",
                table: "Receipts",
                column: "SpaceportID");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_SpacePorts_SpaceportID",
                table: "Receipts",
                column: "SpaceportID",
                principalTable: "SpacePorts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_SpacePorts_SpaceportID",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_SpaceportID",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "SpaceportID",
                table: "Receipts");
        }
    }
}
