using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHome.Data.Migrations
{
    public partial class ChangedForeignKeyFamilyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_AspNetUsers_Families_FamilyId", "Families");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Families_FamilyId",
                table: "AspNetUsers",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id");
        }
    }
}
