using System.Threading.Tasks;

namespace LT.SO.Infra.Data.Common.Mongo
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}