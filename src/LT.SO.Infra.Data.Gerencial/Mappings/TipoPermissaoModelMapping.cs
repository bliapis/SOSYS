using LT.SO.Domain.Permissoes.Permissao.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LT.SO.Infra.Data.Gerencial.Mappings
{
    public class TipoPermissaoModelMapping : IEntityTypeConfiguration<TipoPermissaoModel>
    {
        public void Configure(EntityTypeBuilder<TipoPermissaoModel> builder)
        {
            builder.Property(p => p.Id)
                .IsRequired();

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .HasColumnType("varchar(150)");

            builder.Ignore(p => p.ValidationResult);

            builder.Ignore(p => p.CascadeMode);

            builder.ToTable("TipoPermissao");
        }
    }
}