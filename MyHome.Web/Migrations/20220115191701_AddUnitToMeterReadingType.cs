using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHome.Web.Migrations
{
    public partial class AddUnitToMeterReadingType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "MetersReadingTypes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "MetersReadingTypes");
        }
    }
}
