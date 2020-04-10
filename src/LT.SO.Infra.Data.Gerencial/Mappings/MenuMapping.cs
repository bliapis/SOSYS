using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LT.SO.Domain.Permissoes.Menu.Entities;

namespace LT.SO.Infra.Data.Gerencial.Mappings
{
    public class MenuMapping : IEntityTypeConfiguration<MenuModel>
    {
        public void Configure(EntityTypeBuilder<MenuModel> builder)
        {
            builder.Property(p => p.Id)
                .IsRequired();

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .HasColumnType("varchar(50)");

            builder.Ignore(p => p.ValidationResult);

            builder.Ignore(p => p.CascadeMode);

            builder.ToTable("MenuApp");

            builder.HasOne(m => m.MenuPai)
                    .WithMany(t => t.MenusFilhos)
                    .HasForeignKey(e => e.MenuPaiId);
        }
    }
}