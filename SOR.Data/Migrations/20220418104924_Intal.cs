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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Office = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NumberPhone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    TimeDelete = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreateUser = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PassWord = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Identification = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    IPCreate = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsAdmin = table.Column<int>(type: "int", nullable: true),
                    FistLogin = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndLogin = table.Column<DateTime>(type: "datetime", nullable: true),
                    AgenciesId = table.Column<int>(type: "int", maxLength: 50, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    TimeDelete = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreateUser = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Agencies_AgenciesId",
                        column: x => x.AgenciesId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Historys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryOperation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TimeOperation = table.Column<DateTime>(type: "datetime", nullable: true),
                    Operation = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    TimeDelete = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreateUser = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Historys_User_CreateUser",
                        column: x => x.CreateUser,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NewsLabel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    TimeDelete = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreateUser = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsLabel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsLabel_User_CreateUser",
                        column: x => x.CreateUser,
                        principalTable: "User",
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
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    TimeDelete = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreateUser = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportResult_User_CreateUser",
                        column: x => x.CreateUser,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsState = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    TimeDelete = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreateUser = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                    table.ForeignKey(
                        name: "FK_State_User_CreateUser",
                        column: x => x.CreateUser,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NewsLabelId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserAngel = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LocationReport = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    LocationUser = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Views = table.Column<int>(type: "int", nullable: true),
                    DateSolve = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsStatus = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    TimeDelete = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreateUser = table.Column<int>(type: "int", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Report_User_CreateUser",
                        column: x => x.CreateUser,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportProof",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Proof = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    ReportId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    TimeDelete = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreateUser = table.Column<int>(type: "int", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_ReportProof_User_CreateUser",
                        column: x => x.CreateUser,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Historys_CreateUser",
                table: "Historys",
                column: "CreateUser");

            migrationBuilder.CreateIndex(
                name: "IX_NewsLabel_CreateUser",
                table: "NewsLabel",
                column: "CreateUser");

            migrationBuilder.CreateIndex(
                name: "IX_Report_CreateUser",
                table: "Report",
                column: "CreateUser");

            migrationBuilder.CreateIndex(
                name: "IX_Report_NewsLabelId",
                table: "Report",
                column: "NewsLabelId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportProof_CreateUser",
                table: "ReportProof",
                column: "CreateUser");

            migrationBuilder.CreateIndex(
                name: "IX_ReportProof_ReportId",
                table: "ReportProof",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportResult_CreateUser",
                table: "ReportResult",
                column: "CreateUser");

            migrationBuilder.CreateIndex(
                name: "IX_State_CreateUser",
                table: "State",
                column: "CreateUser");

            migrationBuilder.CreateIndex(
                name: "IX_User_AgenciesId",
                table: "User",
                column: "AgenciesId",
                unique: true,
                filter: "[AgenciesId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Historys");

            migrationBuilder.DropTable(
                name: "ReportProof");

            migrationBuilder.DropTable(
                name: "ReportResult");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "NewsLabel");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Agencies");
        }
    }
}
