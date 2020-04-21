using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using FluentValidation.Results;
using LT.SO.Domain.Core.Bus;
using LT.SO.Domain.Core.Interfaces;
using LT.SO.Domain.Core.Notifications;

namespace LT.SO.Domain.Gerencial.CommandHandlers
{
    public abstract class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        protected List<DomainNotification> _notifications;

        protected CommandHandler(
            IBus bus, 
            IUnitOfWork uow,
            IDomainNotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _notifications = new List<DomainNotification>();
        }

        protected void NotificarValidacoesErro(ValidationResult validationResult)
        {
            NotificarValidacoesErro(null, validationResult);
        }

        protected void NotificarValidacoesErro(Guid commandId, string key, string error)
        {
            _notifications.Add(new DomainNotification(key, error, commandId));
        }

        protected void NotificarValidacoesErro(Guid? commandId, ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                _notifications.Add(commandId != null ? 
                    new DomainNotification(error.PropertyName, error.ErrorMessage, (Guid)commandId)
                    : new DomainNotification(error.PropertyName, error.ErrorMessage));
            }
        }

        protected bool Commit()
        {
            if (_notifications.Any()) return false;

            var commandResponse = _uow.Commit();

            if (commandResponse.Success) return true;

            _notifications.Add(new DomainNotification("Commit", "Ocorreu um erro ao salvar os dados no banco"));

            return false;
        }

        protected void ResetNotificacoes()
        {
            this._notifications = new List<DomainNotification>();
        }

        protected abstract Task SalvarNotificacoes();
    }
}