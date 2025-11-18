using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeYoDAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCardDesign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HtmlDataContent",
                table: "TeacherCardsAttachements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "QRBackSizePixels",
                table: "TeacherCardsAttachements",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "QRFrontSizePercent",
                table: "TeacherCardsAttachements",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HtmlDataContent",
                table: "TeacherCardsAttachements");

            migrationBuilder.DropColumn(
                name: "QRBackSizePixels",
                table: "TeacherCardsAttachements");

            migrationBuilder.DropColumn(
                name: "QRFrontSizePercent",
                table: "TeacherCardsAttachements");
        }
    }
}
