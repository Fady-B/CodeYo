using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeYoDAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class TeacherCardsAttachementsAndSomeRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EnName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "TeacherCardsAttachements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FrontCardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BackCardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardWidth = table.Column<float>(type: "real", nullable: false),
                    CardHeight = table.Column<float>(type: "real", nullable: false),
                    IsQRInFrontCard = table.Column<bool>(type: "bit", nullable: false),
                    QRFrontTopPixels = table.Column<float>(type: "real", nullable: false),
                    QRFrontLeftPixels = table.Column<float>(type: "real", nullable: false),
                    IsQRInBackCard = table.Column<bool>(type: "bit", nullable: false),
                    QRBackTopPixels = table.Column<float>(type: "real", nullable: false),
                    QRBackLeftPixels = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherCardsAttachements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherCardsAttachements_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherStudents",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherStudents", x => new { x.StudentId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_TeacherStudents_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherStudents_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCardsAttachements_TeacherId",
                table: "TeacherCardsAttachements",
                column: "TeacherId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherStudents_TeacherId",
                table: "TeacherStudents",
                column: "TeacherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeacherCardsAttachements");

            migrationBuilder.DropTable(
                name: "TeacherStudents");

            migrationBuilder.AlterColumn<string>(
                name: "EnName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
