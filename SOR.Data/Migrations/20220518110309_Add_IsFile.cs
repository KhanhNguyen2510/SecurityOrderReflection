using Microsoft.EntityFrameworkCore.Migrations;

namespace SOR.Data.Migrations
{
    public partial class Add_IsFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsFile",
                table: "ReportProof",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFile",
                table: "ReportProof");
        }
    }
}
