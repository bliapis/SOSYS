using Microsoft.EntityFrameworkCore;
using LT.SO.Domain.Gerencial.Usuario.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LT.SO.Infra.Data.Gerencial.Mappings
{
    public class UsuarioModelMapping : IEntityTypeConfiguration<UsuarioModel>
    {
        public void Configure(EntityTypeBuilder<UsuarioModel> builder)
        {
            builder.Property(p => p.Id)
                .IsRequired();

            builder.HasKey(p => p.Id);

            builder.Property(p => p.AspNetUserId)
                .IsRequired();

            builder.Property(p => p.Nome)
                .HasColumnType("varchar(50)");

            builder.Ignore(p => p.ValidationResult);

            builder.Ignore(p => p.CascadeMode);

            builder.ToTable("UsuarioApp");
        }
    }
}