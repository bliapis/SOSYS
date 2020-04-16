using System.Threading.Tasks;

namespace LT.SO.Infra.Data.Common.Mongo
{
    public interface IDatabaseSeeder<T> : IDatabaseSeeder
    {
        
    }

    public interface IDatabaseSeeder
    {
        Task SeedAsync();
    }
}