using Microsoft.EntityFrameworkCore.Migrations;

namespace LT.SO.Infra.CrossCutting.Identity.Migrations
{
    public partial class add_campo_active : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioAplicacaoId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioAplicacaoId",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
