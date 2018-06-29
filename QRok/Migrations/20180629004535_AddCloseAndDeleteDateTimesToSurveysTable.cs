using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QRok.Migrations
{
    public partial class AddCloseAndDeleteDateTimesToSurveysTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CloseDateTime",
                table: "Surveys",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDateTime",
                table: "Surveys",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseDateTime",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "DeleteDateTime",
                table: "Surveys");
        }
    }
}
