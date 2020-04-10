using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LT.SO.Infra.CrossCutting.Log.Entities;

namespace LT.SO.Infra.Data.Log.Mappings
{
    public class LogAuditoriaMapping : IEntityTypeConfiguration<LogAuditoria>
    {
        public void Configure(EntityTypeBuilder<LogAuditoria> builder)
        {
            builder.Property(p => p.Id)
                .IsRequired();

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Source)
                .HasColumnType("varchar(50)");

            builder.Property(p => p.Type)
                .HasColumnType("varchar(50)");

            builder.Ignore(p => p.ValidationResult);

            builder.Ignore(p => p.CascadeMode);

            builder.ToTable("LogAuditoria");
        }
    }
}