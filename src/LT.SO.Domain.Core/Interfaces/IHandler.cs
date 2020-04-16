using System.Threading.Tasks;

namespace LT.SO.Domain.Core.Interfaces
{
    public interface IHandler<in T> where T : Message
    {
        void Handle(T message);
        Task HandleAsync(T message);
    }
}