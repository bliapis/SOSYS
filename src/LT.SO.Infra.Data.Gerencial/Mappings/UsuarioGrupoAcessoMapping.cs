using Microsoft.EntityFrameworkCore;
using LT.SO.Domain.Gerencial.Usuario.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LT.SO.Infra.Data.Gerencial.Mappings
{
    public class UsuarioGrupoAcessoMapping : IEntityTypeConfiguration<UsuarioGrupoAcesso>
    {
        public void Configure(EntityTypeBuilder<UsuarioGrupoAcesso> builder)
        {
            builder.ToTable("UsuarioGrupoAcesso");

            builder.HasKey(t => new { t.UsuarioId, t.GrupoAcessoId });

            builder.HasOne(pt => pt.Usuario)
                .WithMany(p => p.UsuarioGruposAcesso)
                .HasForeignKey(e => e.UsuarioId);

            builder.HasOne(pt => pt.GrupoAcesso)
                .WithMany(t => t.GruposAcessoUsuario)
                .HasForeignKey(pt => pt.GrupoAcessoId);
        }
    }
}