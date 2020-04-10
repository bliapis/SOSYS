using LT.SO.Domain.Core.Events;

namespace LT.SO.Domain.Core.Bus
{
    public interface IBus
    {
        void RaiseEvent<T>(T theEvent) where T : Event;
    }
}