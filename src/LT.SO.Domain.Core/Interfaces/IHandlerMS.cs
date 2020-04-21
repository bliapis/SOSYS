using System.Threading.Tasks;

namespace LT.SO.Domain.Core.Interfaces
{
    public interface IHandlerMS<in T> where T : Message
    {
        Task HandleAsync(T message);
    }
}