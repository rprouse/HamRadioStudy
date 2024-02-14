using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HamRadioStudy.Migrations
{
    /// <inheritdoc />
    public partial class Questions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EnglishQuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnglishCorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnglishIncorrectAnswers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FrenchQuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FrenchCorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FrenchIncorrectAnswers = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");
        }
    }
}
