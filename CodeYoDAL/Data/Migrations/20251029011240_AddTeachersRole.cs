using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeYoDAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTeachersRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "security",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "32dcc3d9-17cf-44a8-aad8-69af8cd29277", "c05658e7-7e35-4f87-a132-327d0aac7a27", "Teachers", "TEACHERS" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "32dcc3d9-17cf-44a8-aad8-69af8cd29277");
        }
    }
}
