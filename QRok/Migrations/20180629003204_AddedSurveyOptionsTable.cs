using Microsoft.EntityFrameworkCore.Migrations;

namespace QRok.Migrations
{
    public partial class AddedSurveyOptionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SurveyOptions",
                columns: table => new
                {
                    SurveyId = table.Column<int>(nullable: false),
                    OptionNumber = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyOptions", x => new { x.SurveyId, x.OptionNumber });
                    table.ForeignKey(
                        name: "FK_SurveyOptions_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyOptions");
        }
    }
}
