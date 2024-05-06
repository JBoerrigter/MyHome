using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHome.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedUserForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Families_FamilyId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "FamilyId",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Families_FamilyId",
                table: "AspNetUsers",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Families_FamilyId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "FamilyId",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Families_FamilyId",
                table: "AspNetUsers",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
