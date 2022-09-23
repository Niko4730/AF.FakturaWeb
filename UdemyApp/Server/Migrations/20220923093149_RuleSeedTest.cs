using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UdemyApp.Server.Migrations
{
    public partial class RuleSeedTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Rules",
                columns: new[] { "Id", "Description", "Occurrence", "Title", "UserId" },
                values: new object[] { 1, "This is a test", "Every damn day", "TestRule", 1 });

            migrationBuilder.InsertData(
                table: "Rules",
                columns: new[] { "Id", "Description", "Occurrence", "Title", "UserId" },
                values: new object[] { 2, "This is a test1", "Every damn day1", "TestRule1", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rules",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rules",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
