using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LT.SO.Infra.Data.Log.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogAuditoria",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Identifier = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    Source = table.Column<string>(type: "varchar(50)", nullable: true),
                    Type = table.Column<string>(type: "varchar(50)", nullable: true),
                    Data = table.Column<DateTime>(nullable: true),
                    User = table.Column<string>(nullable: true),
                    Hostname = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Controller = table.Column<string>(nullable: true),
                    Method = table.Column<string>(nullable: true),
                    StatusCode = table.Column<int>(nullable: false),
                    Cookies = table.Column<string>(nullable: true),
                    ServerVariables = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogAuditoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogErro",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Version = table.Column<string>(nullable: true),
                    Method = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Severity = table.Column<string>(nullable: true),
                    User = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Data = table.Column<DateTime>(nullable: true),
                    StatusCode = table.Column<int>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Hostname = table.Column<string>(nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    Application = table.Column<string>(nullable: true),
                    LogQueryStrings = table.Column<string>(nullable: true),
                    LogForms = table.Column<string>(nullable: true),
                    LogCookies = table.Column<string>(nullable: true),
                    LogServerVariables = table.Column<string>(nullable: true),
                    LogDatas = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogErro", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogAuditoria");

            migrationBuilder.DropTable(
                name: "LogErro");
        }
    }
}
