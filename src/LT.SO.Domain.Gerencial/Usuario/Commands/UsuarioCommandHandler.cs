using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using LT.SO.Domain.Core.Bus;
using LT.SO.Domain.Core.Interfaces;
using LT.SO.Domain.Core.Notifications;
using LT.SO.Domain.Core.Repository;
using LT.SO.Domain.Gerencial.CommandHandlers;
using LT.SO.Domain.Gerencial.Usuario.Events;
using LT.SO.Domain.Gerencial.Usuario.Entities;
using LT.SO.Domain.Gerencial.Usuario.Interfaces.Repository;
using LT.SO.Infra.CrossCutting.Identity.Authorization;
using LT.SO.Infra.CrossCutting.Identity.Data;
using LT.SO.Infra.CrossCutting.Identity.Services;
using System.Collections.Generic;
using System.Linq;

namespace LT.SO.Domain.Gerencial.Usuario.Commands
{
    public class UsuarioCommandHandler : CommandHandler,
        IHandlerMS<CreateUsuarioCommand>
    {
        private readonly IBus _bus;
        private readonly IBusMS _busMS;
        private readonly IUnitOfWork _uow;
        private readonly IEventRepository<DomainNotification> _dnRepo;
        private readonly IUsuarioRepository _userRepo;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly IEmailSender _emailSender;
        private readonly JwtTokenOptions _jwtTokenOptions;

        public UsuarioCommandHandler(
            IBus bus,
            IBusMS busMS,
            IUnitOfWork uow,
            IDomainNotificationHandler<DomainNotification> notifications,
            IEventRepository<DomainNotification> dnRepo,
            IUsuarioRepository userRepo,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<JwtTokenOptions> jwtTokenOptions
            //IEmailSender emailSender
            ) : base(bus, uow, notifications)
        {
            _bus = bus;
            _busMS = busMS;
            _uow = uow;
            _dnRepo = dnRepo;
            _userRepo = userRepo;

            _userManager = userManager;
            _signInManager = signInManager;
            //_emailSender = emailSender;
            _jwtTokenOptions = jwtTokenOptions.Value;
            //ThrowIfInvalidOptions(_jwtTokenOptions);
        }

        public async Task HandleAsync(CreateUsuarioCommand message)
        {
            //TODO: Remover a parte de identity do dominio e passar para o service app''1
            // Cria usuário no Identity
            var userIdentity = new ApplicationUser { UserName = message.Usuario, Email = message.Email, Active = true, FirstPass = true };
            var result = await _userManager.CreateAsync(userIdentity, message.Senha);

            if (!result.Succeeded)
            {
                NotificarValidacoesErroIdentity(message.Id, result.Errors);
                await SalvarNotificacoes();
                await _busMS.PublishAsync(new CreateUsuarioRejectedEvent(message.Id, message.Nome, message.CPF, message.Email, message.Ativo, message.DataCadastro, message.AspNetUserId));
            }

            //Cria usuário de aplicação
            var usuario = UsuarioModel.UsuarioModelFactory.NovoUsuarioCompleto(message.Id, message.Nome, message.CPF, message.Email, message.AspNetUserId, message.DataCadastro, message.Ativo);
            usuario.SetAspNetUserId(userIdentity.Id);
            usuario.AtivarUsuario();

            if (!UsuarioValido(usuario)) return;

            _userRepo.Add(usuario);

            if (Commit())
                await _busMS.PublishAsync(new UsuarioCreatedEvent(usuario.Id, usuario.Nome, usuario.CPF, usuario.Email, usuario.Ativo, usuario.DataCadastro, usuario.AspNetUserId));
            else
            {
                await SalvarNotificacoes();
                await _busMS.PublishAsync(new CreateUsuarioRejectedEvent(message.Id, message.Nome, message.CPF, message.Email, message.Ativo, message.DataCadastro, message.AspNetUserId));
            };
        }

        private bool UsuarioValido(UsuarioModel usuario)
        {
            if (usuario.IsValid()) return true;

            NotificarValidacoesErro(usuario.Id, usuario.ValidationResult);
            SalvarNotificacoes().Wait();
            return false;
        }

        protected async override Task SalvarNotificacoes()
        {
            if (!_notifications.Any()) return;

            foreach (var dn in _notifications) // TODO: melhorar essa implementação acrescentando um método no repo para add list T
            {
                await _dnRepo.AddAsync(dn);
            }

            ResetNotificacoes();
        }

        private void NotificarValidacoesErroIdentity(Guid commandId, IEnumerable<IdentityError> indentErrors)
        {
            foreach (var error in indentErrors)
            {
                NotificarValidacoesErro(commandId, error.Code, error.Description);
            }
        }

        //private static void ThrowIfInvalidOptions(JwtTokenOptions options)
        //{
        //    if (options == null) throw new ArgumentNullException(nameof(options));
        //
        //    if (options.ValidFor <= TimeSpan.Zero)
        //        throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtTokenOptions.ValidFor));
        //
        //    if (options.SigningCredentials == null)
        //        throw new ArgumentNullException(nameof(JwtTokenOptions.SigningCredentials));
        //
        //    if (options.JtiGenerator == null)
        //        throw new ArgumentNullException(nameof(JwtTokenOptions.JtiGenerator));
        //}
    }
}