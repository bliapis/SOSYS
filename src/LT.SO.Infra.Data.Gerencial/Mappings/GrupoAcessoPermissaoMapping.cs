using LT.SO.Domain.Permissoes.GrupoAcesso.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LT.SO.Infra.Data.Gerencial.Mappings
{
    public class GrupoAcessoPermissaoMapping : IEntityTypeConfiguration<GrupoAcessoPermissao>
    {
        public void Configure(EntityTypeBuilder<GrupoAcessoPermissao> builder)
        {
            builder.ToTable("GrupoAcessoPermissao");

            builder.HasKey(t => new { t.GrupoAcessoId, t.PermissaoId });

            builder.HasOne(pt => pt.GrupoAcesso)
                .WithMany(p => p.GruposAcessoPermissaos)
                .HasForeignKey(pt => pt.GrupoAcessoId);

            builder.HasOne(pt => pt.Permissao)
                .WithMany(t => t.GruposAcessoPermissaos)
                .HasForeignKey(pt => pt.PermissaoId);
        }
    }
}