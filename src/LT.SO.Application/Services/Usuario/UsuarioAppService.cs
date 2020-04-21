using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using LT.SO.Domain.Core.Bus;
using LT.SO.Domain.Core.Notifications;
using LT.SO.Domain.Gerencial.Usuario.Commands;
using LT.SO.Application.ViewModels.Gerencial.Usuario;
using Microsoft.Extensions.Options;
using LT.SO.Infra.CrossCutting.Identity.Authorization;
using LT.SO.Infra.CrossCutting.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using LT.SO.Domain.Gerencial.Usuario.Interfaces.Repository;

namespace LT.SO.Application.Services.Usuario
{
    public class UsuarioAppService : ServiceBase, IUsuarioAppService
    {
        private readonly IBus _bus;
        private readonly IBusMS _busMS;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtTokenOptions _jwtTokenOptions;
        private readonly IUsuarioRepository _userRepo;

        public UsuarioAppService(
            IBus bus,
            IBusMS busMS,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<JwtTokenOptions> jwtTokenOptions,
            IDomainNotificationHandler<DomainNotification> notifications,
            IUsuarioRepository userRepo) : base(bus, notifications)
        {
            _bus = bus;
            _busMS = busMS;
            _mapper = mapper;
            _userRepo = userRepo;

            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenOptions = jwtTokenOptions.Value;
            ThrowIfInvalidOptions(_jwtTokenOptions);
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        public async Task<Guid> Cadastrar(AddUsuarioViewModel usuarioViewModel)
        {
            var createCommand = _mapper.Map<CreateUsuarioCommand>(usuarioViewModel);

            await _busMS.PublishAsync(createCommand);

            return createCommand.Id;
        }

        public void Atualizar(UsuarioViewModel usuarioViewModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UsuarioViewModel> ObterTodos()
        {
            throw new NotImplementedException();
        }

        public async Task<object> Login(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null && user.Active)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    var response = await GenerateUserToken(model);
                    return response;
                }

                if (_userManager.IsLockedOutAsync(user).Result)
                {
                    NotificarErro("2", "Seu usuário foi bloqueado por 10 minutos, devido ao excesso de tentativas de acesso com erro.");
                }
            }

            NotificarErro("2", "Falha ao realizar o login.");
            return model;
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

        private async Task<object> GenerateUserToken(LoginViewModel login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            var userClaims = await _userManager.GetClaimsAsync(user);

            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.NameId, user.Id));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.GivenName, user.NormalizedUserName));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, await _jwtTokenOptions.JtiGenerator()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtTokenOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64));

            var claimsUser = new List<Claim>(); //TODO: Implementar após refactory: CarregarPermissoes(_usuarioService.ObterPorAspNetUserId(user.Id));

            foreach (var claim in claimsUser)
                userClaims.Add(claim);

            var jwt = new JwtSecurityToken(
                issuer: _jwtTokenOptions.Issuer,
                audience: _jwtTokenOptions.Audience,
                claims: userClaims,
                notBefore: _jwtTokenOptions.NotBefore,
                expires: _jwtTokenOptions.Expiration,
                signingCredentials: _jwtTokenOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = DateTimeOffset.UtcNow.AddHours((_jwtTokenOptions.Expiration - DateTime.Now).TotalHours),//(int)_jwtTokenOptions.ValidFor.TotalSeconds,
                user = new { Usuario = user.UserName, Email = user.Email, FirstPass = user.FirstPass }
            };

            return response;
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