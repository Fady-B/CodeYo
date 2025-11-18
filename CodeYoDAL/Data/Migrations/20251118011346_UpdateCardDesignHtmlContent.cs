using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeYoDAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCardDesignHtmlContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HtmlDataContent",
                table: "TeacherCardsAttachements",
                newName: "FrontHtmlDataContent");

            migrationBuilder.AddColumn<string>(
                name: "BackHtmlDataContent",
                table: "TeacherCardsAttachements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackHtmlDataContent",
                table: "TeacherCardsAttachements");

            migrationBuilder.RenameColumn(
                name: "FrontHtmlDataContent",
                table: "TeacherCardsAttachements",
                newName: "HtmlDataContent");
        }
    }
}
