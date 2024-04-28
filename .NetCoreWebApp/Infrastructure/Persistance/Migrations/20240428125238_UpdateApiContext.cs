using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    public partial class UpdateApiContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "ParkingLot",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingLot_AppUserId",
                table: "ParkingLot",
                column: "AppUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingLot_AppUsers_AppUserId",
                table: "ParkingLot",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingLot_AppUsers_AppUserId",
                table: "ParkingLot");

            migrationBuilder.DropIndex(
                name: "IX_ParkingLot_AppUserId",
                table: "ParkingLot");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "ParkingLot");
        }
    }
}
