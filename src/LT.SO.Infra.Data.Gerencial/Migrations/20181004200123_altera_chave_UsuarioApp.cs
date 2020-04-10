using Microsoft.EntityFrameworkCore.Migrations;

namespace LT.SO.Infra.Data.Gerencial.Migrations
{
    public partial class altera_chave_UsuarioApp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioGrupoAcesso_UsuarioApp_UsuarioId",
                table: "UsuarioGrupoAcesso");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioApp",
                table: "UsuarioApp");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UsuarioApp_Id",
                table: "UsuarioApp");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioApp",
                table: "UsuarioApp",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioGrupoAcesso_UsuarioApp_UsuarioId",
                table: "UsuarioGrupoAcesso",
                column: "UsuarioId",
                principalTable: "UsuarioApp",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioGrupoAcesso_UsuarioApp_UsuarioId",
                table: "UsuarioGrupoAcesso");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioApp",
                table: "UsuarioApp");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioApp",
                table: "UsuarioApp",
                column: "AspNetUserId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UsuarioApp_Id",
                table: "UsuarioApp",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioGrupoAcesso_UsuarioApp_UsuarioId",
                table: "UsuarioGrupoAcesso",
                column: "UsuarioId",
                principalTable: "UsuarioApp",
                principalColumn: "AspNetUserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
