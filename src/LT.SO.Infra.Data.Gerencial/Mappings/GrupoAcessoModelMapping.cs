using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LT.SO.Domain.Permissoes.GrupoAcesso.Entities;

namespace LT.SO.Infra.Data.Gerencial.Mappings
{
    public class GrupoAcessoModelMapping : IEntityTypeConfiguration<GrupoAcessoModel>
    {
        public void Configure(EntityTypeBuilder<GrupoAcessoModel> builder)
        {
            builder.Property(p => p.Id)
                .IsRequired();

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .HasColumnType("varchar(50)");

            builder.Ignore(p => p.ValidationResult);

            builder.Ignore(p => p.CascadeMode);

            builder.ToTable("GrupoAcesso");
        }
    }
}