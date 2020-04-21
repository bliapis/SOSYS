using System.Threading.Tasks;

namespace LT.SO.Domain.Core.Repository
{
    public interface IEventRepository<T> : IRepositoryBase
    {
        Task AddAsync(T obj);
    }
}