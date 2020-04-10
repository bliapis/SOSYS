using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LT.SO.Infra.Data.Gerencial.Migrations
{
    public partial class Add_Campo_Usuario_DataCadastro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "UsuarioApp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "UsuarioApp");
        }
    }
}
