using System.Threading.Tasks;

namespace LT.SO.Domain.Core.Bus
{
    public interface IBusMS
    {
        Task PublishAsync<T>(T message) where T : Message;
    }
}