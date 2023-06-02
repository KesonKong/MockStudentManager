using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentManager.DBModels.Migrations
{
    public partial class AdddeletePropetyToStudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "deletePropety",
                table: "Students",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "deletePropety",
                table: "Students");
        }
    }
}
