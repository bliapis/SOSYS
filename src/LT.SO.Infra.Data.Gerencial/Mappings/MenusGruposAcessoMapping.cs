using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LT.SO.Domain.Permissoes.Menu.Entities;

namespace LT.SO.Infra.Data.Gerencial.Mappings
{
    public class MenusGruposAcessoMapping : IEntityTypeConfiguration<MenusGruposAcesso>
    {
        public void Configure(EntityTypeBuilder<MenusGruposAcesso> builder)
        {
            builder.ToTable("MenuAppGruposAcesso");

            builder.HasKey(t => new { t.MenuId, t.GrupoAcessoId });

            builder.HasOne(pt => pt.Menu)
                .WithMany(p => p.MenusGruposAcesso)
                .HasForeignKey(pt => pt.MenuId);

            builder.HasOne(pt => pt.GrupoAcesso)
                .WithMany(t => t.MenusGruposAcesso)
                .HasForeignKey(pt => pt.GrupoAcessoId);
        }
    }
}