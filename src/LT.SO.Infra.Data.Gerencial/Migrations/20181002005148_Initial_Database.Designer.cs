﻿// <auto-generated />
using System;
using LT.SO.Infra.Data.Gerencial.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LT.SO.Infra.Data.Gerencial.Migrations
{
    [DbContext(typeof(GerencialContext))]
    [Migration("20181002005148_Initial_Database")]
    partial class Initial_Database
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LT.SO.Domain.Permissoes.GrupoAcesso.Entities.GrupoAcessoModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("GrupoAcesso");
                });

            modelBuilder.Entity("LT.SO.Domain.Permissoes.GrupoAcesso.Entities.GrupoAcessoPermissao", b =>
                {
                    b.Property<Guid>("GrupoAcessoId");

                    b.Property<Guid>("PermissaoId");

                    b.HasKey("GrupoAcessoId", "PermissaoId");

                    b.HasIndex("PermissaoId");

                    b.ToTable("GrupoAcessoPermissao");
                });

            modelBuilder.Entity("LT.SO.Domain.Permissoes.GrupoAcesso.Entities.GrupoAcessoUsuario", b =>
                {
                    b.Property<Guid>("GrupoAcessoId");

                    b.Property<Guid>("UserId");

                    b.HasKey("GrupoAcessoId", "UserId");

                    b.ToTable("GrupoAcessoUsuario");
                });

            modelBuilder.Entity("LT.SO.Domain.Permissoes.Menu.Entities.MenuModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("MenuPaiId");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("MenuPaiId");

                    b.ToTable("MenuApp");
                });

            modelBuilder.Entity("LT.SO.Domain.Permissoes.Menu.Entities.MenusGruposAcesso", b =>
                {
                    b.Property<Guid>("MenuId");

                    b.Property<Guid>("GrupoAcessoId");

                    b.HasKey("MenuId", "GrupoAcessoId");

                    b.HasIndex("GrupoAcessoId");

                    b.ToTable("MenuAppGruposAcesso");
                });

            modelBuilder.Entity("LT.SO.Domain.Permissoes.Permissao.Entities.PermissaoModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("TipoId");

                    b.Property<string>("Valor")
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("TipoId");

                    b.ToTable("Permissao");
                });

            modelBuilder.Entity("LT.SO.Domain.Permissoes.Permissao.Entities.TipoPermissaoModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.ToTable("TipoPermissao");
                });

            modelBuilder.Entity("LT.SO.Domain.Permissoes.GrupoAcesso.Entities.GrupoAcessoPermissao", b =>
                {
                    b.HasOne("LT.SO.Domain.Permissoes.GrupoAcesso.Entities.GrupoAcessoModel", "GrupoAcesso")
                        .WithMany("GruposAcessoPermissaos")
                        .HasForeignKey("GrupoAcessoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LT.SO.Domain.Permissoes.Permissao.Entities.PermissaoModel", "Permissao")
                        .WithMany("GruposAcessoPermissaos")
                        .HasForeignKey("PermissaoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LT.SO.Domain.Permissoes.GrupoAcesso.Entities.GrupoAcessoUsuario", b =>
                {
                    b.HasOne("LT.SO.Domain.Permissoes.GrupoAcesso.Entities.GrupoAcessoModel", "GrupoAcesso")
                        .WithMany("GruposAcessoUsuarios")
                        .HasForeignKey("GrupoAcessoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LT.SO.Domain.Permissoes.Menu.Entities.MenuModel", b =>
                {
                    b.HasOne("LT.SO.Domain.Permissoes.Menu.Entities.MenuModel", "MenuPai")
                        .WithMany("MenusFilhos")
                        .HasForeignKey("MenuPaiId");
                });

            modelBuilder.Entity("LT.SO.Domain.Permissoes.Menu.Entities.MenusGruposAcesso", b =>
                {
                    b.HasOne("LT.SO.Domain.Permissoes.GrupoAcesso.Entities.GrupoAcessoModel", "GrupoAcesso")
                        .WithMany("MenusGruposAcesso")
                        .HasForeignKey("GrupoAcessoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LT.SO.Domain.Permissoes.Menu.Entities.MenuModel", "Menu")
                        .WithMany("MenusGruposAcesso")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LT.SO.Domain.Permissoes.Permissao.Entities.PermissaoModel", b =>
                {
                    b.HasOne("LT.SO.Domain.Permissoes.Permissao.Entities.TipoPermissaoModel", "Tipo")
                        .WithMany("Permissoes")
                        .HasForeignKey("TipoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
