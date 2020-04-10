using LT.SO.Domain.Permissoes.GrupoAcesso.Entities;
using LT.SO.Domain.Permissoes.Permissao.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LT.SO.Infra.Data.Gerencial.Mappings
{
    public class PermissaoModelMapping : IEntityTypeConfiguration<PermissaoModel>
    {
        public void Configure(EntityTypeBuilder<PermissaoModel> builder)
        {
            builder.Property(p => p.Id)
                .IsRequired();

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Valor)
                .HasColumnType("varchar(30)");

            builder.Ignore(p => p.ValidationResult);

            builder.Ignore(p => p.CascadeMode);

            builder.ToTable("Permissao");

            builder.HasOne(e => e.Tipo)
                .WithMany(o => o.Permissoes)
                .HasForeignKey(e => e.TipoId);
        }
    }
}