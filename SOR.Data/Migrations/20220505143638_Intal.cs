using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SOR.Data.Migrations
{
    public partial class Intal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agencies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Office = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NumberPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    TimeDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreateUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Code",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Key = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    TimeDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreateUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Code", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryOperation = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    TimeOperation = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsOperation = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    TimeDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreateUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsLabel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    TimeDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreateUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsLabel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsState = table.Column<int>(type: "int", nullable: false),
                    ReportId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    TimeDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreateUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PassWord = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NumberPhone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Identification = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    IPCreate = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsAdmin = table.Column<int>(type: "int", nullable: false),
                    FistLogin = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndLogin = table.Column<DateTime>(type: "datetime", nullable: true),
                    AgenciesId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    TimeDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreateUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NewsLabelId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IsReport = table.Column<int>(type: "int", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserAngel = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    LocationReport = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    LocationUser = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Views = table.Column<int>(type: "int", nullable: false),
                    DateSolve = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsStatus = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    TimeDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreateUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_NewsLabel_NewsLabelId",
                        column: x => x.NewsLabelId,
                        principalTable: "NewsLabel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportProof",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Proof = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    TimeDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreateUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportProof", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportProof_Report_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Report",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportResult",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    ReportId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    TimeDelete = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreateUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportResult_Report_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Report",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Report_NewsLabelId",
                table: "Report",
                column: "NewsLabelId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportProof_ReportId",
                table: "ReportProof",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportResult_ReportId",
                table: "ReportResult",
                column: "ReportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agencies");

            migrationBuilder.DropTable(
                name: "Code");

            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropTable(
                name: "ReportProof");

            migrationBuilder.DropTable(
                name: "ReportResult");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "NewsLabel");
        }
    }
}
