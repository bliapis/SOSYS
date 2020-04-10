using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LT.SO.Infra.Data.Gerencial.Extensions
{
    public abstract class EntityTypeConfiguration<TEntity> where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }
}