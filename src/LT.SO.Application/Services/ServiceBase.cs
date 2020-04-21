using LT.SO.Domain.Core.Bus;
using LT.SO.Domain.Core.Notifications;

namespace LT.SO.Application.Services
{
    public class ServiceBase
    {
        private readonly IBus _bus;
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;

        public ServiceBase(
            IBus bus,
            IDomainNotificationHandler<DomainNotification> notifications)
        {
            _bus = bus;
            _notifications = notifications;
        }

        protected void NotificarErro(string code, string message)
        {
            _bus.RaiseEvent(new DomainNotification(code, message));
        }
    }
}