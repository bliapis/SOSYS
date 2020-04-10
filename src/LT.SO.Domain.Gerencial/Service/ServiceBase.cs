using FluentValidation.Results;
using LT.SO.Domain.Core.Bus;
using LT.SO.Domain.Core.Interfaces;
using LT.SO.Domain.Core.Notifications;

namespace LT.SO.Domain.Gerencial.Service
{
    public abstract class ServiceBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IBus _bus;
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;

        protected ServiceBase(
            IUnitOfWork uow, 
            IBus bus, 
            IDomainNotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _bus = bus;
            _notifications = notifications;
        }

        protected void NotifyErrorValidation(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                _bus.RaiseEvent(new DomainNotification(error.PropertyName, error.ErrorMessage));
            }
        }

        //protected bool Commit()
        protected void Commit()
        {
            //if (_notifications.HasNotifications()) return false;
            //var commandResponse = _uow.Commit();
            //if (commandResponse.Success) return true;

            //_bus.RaiseEvent(new DomainNotification("2", "Ocorreu um erro ao salvar os dados."));
            //return false;


            var commandResponse = _uow.Commit();

            if (!commandResponse.Success)
                _bus.RaiseEvent(new DomainNotification("2", "Ocorreu um erro ao salvar os dados."));
        }
    }
}
