using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeYoDAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityBaseToTeacherCardsAttachements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Cancelled",
                table: "TeacherCardsAttachements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TeacherCardsAttachements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TeacherCardsAttachements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "TeacherCardsAttachements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "TeacherCardsAttachements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cancelled",
                table: "TeacherCardsAttachements");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TeacherCardsAttachements");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TeacherCardsAttachements");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "TeacherCardsAttachements");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "TeacherCardsAttachements");
        }
    }
}
