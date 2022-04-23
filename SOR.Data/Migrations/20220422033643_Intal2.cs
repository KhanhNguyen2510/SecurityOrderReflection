using Microsoft.EntityFrameworkCore.Migrations;

namespace SOR.Data.Migrations
{
    public partial class Intal2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Operation",
                table: "History",
                newName: "IsOperation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsOperation",
                table: "History",
                newName: "Operation");
        }
    }
}
