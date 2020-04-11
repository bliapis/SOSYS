using System.Threading.Tasks;

namespace LT.SO.Infra.CrossCutting.Log.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T obj);
    }
}
