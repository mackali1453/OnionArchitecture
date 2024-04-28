using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobilePhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tckn = table.Column<long>(type: "bigint", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsLockedOut = table.Column<bool>(type: "bit", nullable: false),
                    LogInTryCount = table.Column<int>(type: "int", nullable: false),
                    LockedUpExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppVehicle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehiclePlate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    AppUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppVehicle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppVehicle_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppVehicle_AppUserId",
                table: "AppVehicle",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppVehicle");

            migrationBuilder.DropTable(
                name: "AppUsers");
        }
    }
}
