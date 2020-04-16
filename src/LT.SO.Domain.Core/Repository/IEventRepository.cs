using System.Threading.Tasks;

namespace LT.SO.Domain.Core.Repository
{
    public interface IEventRepository<T>
    {
        Task AddAsync(T obj);
    }
}