using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LT.SO.Infra.Data.Gerencial.Migrations
{
    public partial class Add_Fk_Usuario_GrupoAcesso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GrupoAcessoUsuario");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "UsuarioApp",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "UsuarioGrupoAcesso",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(nullable: false),
                    GrupoAcessoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioGrupoAcesso", x => new { x.UsuarioId, x.GrupoAcessoId });
                    table.ForeignKey(
                        name: "FK_UsuarioGrupoAcesso_GrupoAcesso_GrupoAcessoId",
                        column: x => x.GrupoAcessoId,
                        principalTable: "GrupoAcesso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioGrupoAcesso_UsuarioApp_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "UsuarioApp",
                        principalColumn: "AspNetUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioGrupoAcesso_GrupoAcessoId",
                table: "UsuarioGrupoAcesso",
                column: "GrupoAcessoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioGrupoAcesso");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "UsuarioApp");

            migrationBuilder.CreateTable(
                name: "GrupoAcessoUsuario",
                columns: table => new
                {
                    GrupoAcessoId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoAcessoUsuario", x => new { x.GrupoAcessoId, x.UserId });
                    table.ForeignKey(
                        name: "FK_GrupoAcessoUsuario_GrupoAcesso_GrupoAcessoId",
                        column: x => x.GrupoAcessoId,
                        principalTable: "GrupoAcesso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
