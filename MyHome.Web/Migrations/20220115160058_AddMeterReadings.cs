using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHome.Web.Migrations
{
    public partial class AddMeterReadings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MetersReadingTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetersReadingTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MetersReadings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Image = table.Column<byte[]>(type: "BLOB", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReadingTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetersReadings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MetersReadings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MetersReadings_MetersReadingTypes_ReadingTypeId",
                        column: x => x.ReadingTypeId,
                        principalTable: "MetersReadingTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MetersReadings_ReadingTypeId",
                table: "MetersReadings",
                column: "ReadingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MetersReadings_UserId",
                table: "MetersReadings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MetersReadings");

            migrationBuilder.DropTable(
                name: "MetersReadingTypes");
        }
    }
}
