using Microsoft.EntityFrameworkCore.Migrations;

namespace LT.SO.Infra.CrossCutting.Identity.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioAplicacaoId",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioAplicacaoId",
                table: "AspNetUsers");
        }
    }
}
