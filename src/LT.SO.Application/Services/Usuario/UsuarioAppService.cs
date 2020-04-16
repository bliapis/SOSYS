using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using AutoMapper;
using LT.SO.Application.ViewModels.Gerencial.Usuario;
using LT.SO.Domain.Core.Bus;
using LT.SO.Infra.CrossCutting.Identity.Data;
using LT.SO.Infra.CrossCutting.Identity.Services;
using LT.SO.Infra.CrossCutting.Identity.Authorization;

namespace LT.SO.Application.Services.Usuario
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IBus _bus;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtTokenOptions _jwtTokenOptions;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public UsuarioAppService(
            IBus bus,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<JwtTokenOptions> jwtTokenOptions,
            IMapper mapper,
            IEmailSender emailSender)
        {
            _bus = bus;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenOptions = jwtTokenOptions.Value;
            ThrowIfInvalidOptions(_jwtTokenOptions);
            _mapper = mapper;
            _emailSender = emailSender;
        }

        public void Cadastrar(AddUsuarioViewModel usuarioViewModel)
        {
            throw new NotImplementedException();
        }

        public void Atualizar(UsuarioViewModel usuarioViewModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UsuarioViewModel> ObterTodos()
        {
            throw new NotImplementedException();
        }

        public LoginViewModel Login(LoginViewModel model)
        {
            throw new NotImplementedException();
        }



        public void Desativar(Guid id)
        {
            throw new NotImplementedException();
        }

        public void ForgotPassword(ForgotPasswordViewModel model)
        {
            throw new NotImplementedException();
        }

        public void NewPassword(NewPasswordViewModel model)
        {
            throw new NotImplementedException();
        }

        public void ResetPassword(ResetPasswordViewModel model)
        {
            throw new NotImplementedException();
        }

        private static void ThrowIfInvalidOptions(JwtTokenOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtTokenOptions.ValidFor));

            if (options.SigningCredentials == null)
                throw new ArgumentNullException(nameof(JwtTokenOptions.SigningCredentials));

            if (options.JtiGenerator == null)
                throw new ArgumentNullException(nameof(JwtTokenOptions.JtiGenerator));
        }
    }
}