using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentManager.DBModels.Migrations
{
    public partial class AlterStudentTableData_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "ClassName", "Email", "Name" },
                values: new object[] { 3, 1, "kongyoujia@126.com", "孔佑嘉" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
