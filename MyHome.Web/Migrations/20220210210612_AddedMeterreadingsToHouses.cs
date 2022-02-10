using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHome.Web.Migrations
{
    public partial class AddedMeterreadingsToHouses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HouseId",
                table: "MetersReadings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MetersReadings_HouseId",
                table: "MetersReadings",
                column: "HouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_MetersReadings_Houses_HouseId",
                table: "MetersReadings",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MetersReadings_Houses_HouseId",
                table: "MetersReadings");

            migrationBuilder.DropIndex(
                name: "IX_MetersReadings_HouseId",
                table: "MetersReadings");

            migrationBuilder.DropColumn(
                name: "HouseId",
                table: "MetersReadings");
        }
    }
}
