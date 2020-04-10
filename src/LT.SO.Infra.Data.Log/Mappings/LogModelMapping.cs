using LT.SO.Infra.CrossCutting.Log.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LT.SO.Infra.Data.Log.Mappings
{
    public class LogModelMapping : IEntityTypeConfiguration<LogModel>
    {
        public void Configure(EntityTypeBuilder<LogModel> builder)
        {
            builder.Property(p => p.Id)
                .IsRequired();

            builder.Property(p => p.DateTime)
                .HasColumnName("Data");

            builder.HasKey(g => g.Id);

            builder.Ignore(g => g.ValidationResult);

            builder.Ignore(g => g.CascadeMode);

            builder.ToTable("LogErro");
        }
    }
}