using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LT.SO.Domain.Core.Bus;
using Microsoft.AspNetCore.Identity;
using LT.SO.Domain.Core.Interfaces;
using LT.SO.Domain.Core.Notifications;
using Microsoft.Extensions.Options;
using LT.SO.Services.Api.Models;
using System.Collections.Generic;

namespace LT.SO.Services.Api.Controllers
{
    [Produces("application/json")]
    public abstract class BaseController : ControllerBase
    {
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;
        private readonly IBus _bus;
        private readonly IOptions<AuditConfig> _auditSettings;

        protected Guid UsuarioId { get; set; }
        protected bool Auditar = false;

        protected BaseController(
            IDomainNotificationHandler<DomainNotification> notifications,
            IUser user,
            IBus bus,
            IOptions<AuditConfig> auditConfig)
        {
            _notifications = notifications;
            _bus = bus;
            _auditSettings = auditConfig;

            if (user.IsAuthenticated())
                UsuarioId = user.GetUserId();

            if (_auditSettings.Value.Active)
                Auditar = _auditSettings.Value.Controllers.Contains(GetType().Name);
        }

        protected new IActionResult Response(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(
                    new
                    {
                        success = true,
                        data = result
                    });
            }

            //Todas notificações com key "-1", não podem ser apresentadas ao usuário, apenas logadas.
            return Ok(new
            {
                success = false,
                errors = _notifications.GetNotifications().Where(n => n.Key != "-1").Select(n => n.Value)
            });
        }

        protected bool OperacaoValida()
        {
            return !_notifications.HasNotifications();
        }


        protected void NotificarErroModelInvalida()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);

            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(string.Empty, erroMsg);
            }
        }

        protected void AdicionarErrosIdentity(IdentityResult result)
        {
            foreach (var error in result.Errors)
                _bus.RaiseEvent(new DomainNotification(result.ToString(), error.Description));
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            _bus.RaiseEvent(new DomainNotification(codigo, mensagem));
        }
    }
}