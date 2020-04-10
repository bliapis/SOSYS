using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LT.SO.Infra.Data.Gerencial.Migrations
{
    public partial class Initial_Database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GrupoAcesso",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoAcesso", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuApp",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: true),
                    MenuPaiId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuApp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuApp_MenuApp_MenuPaiId",
                        column: x => x.MenuPaiId,
                        principalTable: "MenuApp",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TipoPermissao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(150)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPermissao", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "MenuAppGruposAcesso",
                columns: table => new
                {
                    MenuId = table.Column<Guid>(nullable: false),
                    GrupoAcessoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuAppGruposAcesso", x => new { x.MenuId, x.GrupoAcessoId });
                    table.ForeignKey(
                        name: "FK_MenuAppGruposAcesso_GrupoAcesso_GrupoAcessoId",
                        column: x => x.GrupoAcessoId,
                        principalTable: "GrupoAcesso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuAppGruposAcesso_MenuApp_MenuId",
                        column: x => x.MenuId,
                        principalTable: "MenuApp",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permissao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Valor = table.Column<string>(type: "varchar(30)", nullable: true),
                    TipoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissao_TipoPermissao_TipoId",
                        column: x => x.TipoId,
                        principalTable: "TipoPermissao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GrupoAcessoPermissao",
                columns: table => new
                {
                    GrupoAcessoId = table.Column<Guid>(nullable: false),
                    PermissaoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoAcessoPermissao", x => new { x.GrupoAcessoId, x.PermissaoId });
                    table.ForeignKey(
                        name: "FK_GrupoAcessoPermissao_GrupoAcesso_GrupoAcessoId",
                        column: x => x.GrupoAcessoId,
                        principalTable: "GrupoAcesso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrupoAcessoPermissao_Permissao_PermissaoId",
                        column: x => x.PermissaoId,
                        principalTable: "Permissao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GrupoAcessoPermissao_PermissaoId",
                table: "GrupoAcessoPermissao",
                column: "PermissaoId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuApp_MenuPaiId",
                table: "MenuApp",
                column: "MenuPaiId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuAppGruposAcesso_GrupoAcessoId",
                table: "MenuAppGruposAcesso",
                column: "GrupoAcessoId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissao_TipoId",
                table: "Permissao",
                column: "TipoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GrupoAcessoPermissao");

            migrationBuilder.DropTable(
                name: "GrupoAcessoUsuario");

            migrationBuilder.DropTable(
                name: "MenuAppGruposAcesso");

            migrationBuilder.DropTable(
                name: "Permissao");

            migrationBuilder.DropTable(
                name: "GrupoAcesso");

            migrationBuilder.DropTable(
                name: "MenuApp");

            migrationBuilder.DropTable(
                name: "TipoPermissao");
        }
    }
}
