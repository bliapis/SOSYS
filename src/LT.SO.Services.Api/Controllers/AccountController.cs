using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AutoMapper;
using Newtonsoft.Json;
using LT.SO.Services.Api.Models;
using LT.SO.Services.Api.ViewModels.Gerencial.Usuario;
using LT.SO.Services.Api.ViewModels.Gerencial.Permissoes;
using LT.SO.Services.Api.ViewModels.Gerencial.GrupoAcesso;
using LT.SO.Domain.Core.Bus;
using LT.SO.Domain.Core.Interfaces;
using LT.SO.Domain.Core.Notifications;
using LT.SO.Domain.Gerencial.Usuario.Entities;
using LT.SO.Domain.Gerencial.Usuario.Interfaces.Service;
using LT.SO.Infra.CrossCutting.Identity.Data;
using LT.SO.Infra.CrossCutting.Identity.Models;
using LT.SO.Infra.CrossCutting.Identity.Authorization;
using LT.SO.Infra.CrossCutting.Log.Interfaces;
using LT.SO.Domain.Permissoes.Permissao.Entities;
using LT.SO.Domain.Permissoes.Permissao.Interfaces.Services;
using LT.SO.Domain.Permissoes.GrupoAcesso.Interfaces.Service;

namespace LT.SO.Services.Api.Controllers
{
    [ApiController]
    [Route("usuario/")]
    public class AccountController : BaseController
    {
        private readonly IBus _bus;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtTokenOptions _jwtTokenOptions;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ILogService _logService;
        private readonly IUsuarioService _usuarioService;
        private readonly IGrupoAcessoService _grupoAcessoService;
        private readonly IPermissaoService _permissaoService;
        private readonly Infra.CrossCutting.Identity.Services.IEmailSender _emailSender;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUser user,
            IBus bus,
            IMapper mapper,
            ILogService logService,
            ILoggerFactory loggerFactory,
            IUsuarioService usuarioService,
            IGrupoAcessoService grupoAcessoService,
            IPermissaoService permissaoService,
            IOptions<JwtTokenOptions> jwtTokenOptions,
            IDomainNotificationHandler<DomainNotification> notifications,
            IOptions<AuditConfig> auditConfig,
            Infra.CrossCutting.Identity.Services.IEmailSender emailSender) : base(notifications, user, bus, auditConfig)
        {
            _bus = bus;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _usuarioService = usuarioService;
            _grupoAcessoService = grupoAcessoService;
            _permissaoService = permissaoService;
            _emailSender = emailSender;

            _jwtTokenOptions = jwtTokenOptions.Value;
            ThrowIfInvalidOptions(_jwtTokenOptions);
            _logService = logService;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);


        [HttpPost]
        [AllowAnonymous]
        [Route("novo")]
        public async Task<IActionResult> Cadastro([FromBody] AddUsuarioViewModel model)
        {
            //if (version == 2)
            //    return Response(new { Message = "API v2 não disponivel" });

            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }
            
            var user = new ApplicationUser { UserName = model.Usuario, Email = model.Email, Active = true, FirstPass = true };
            var result = await _userManager.CreateAsync(user, model.Senha);

            if (result.Succeeded)
            {
                var usuarioModel = _mapper.Map<UsuarioModel>(model);
                usuarioModel.SetAspNetUserId(user.Id);
                usuarioModel.AtivarUsuario();

                _usuarioService.Adicionar(usuarioModel);

                if (!OperacaoValida())
                {
                    await _userManager.DeleteAsync(user);
                    return Response(model);
                }
                
                _logService.SaveAudit(HttpContext.TraceIdentifier, string.Format("Usuário {0} criado com sucesso", model.Nome), JsonConvert.SerializeObject(model), Infra.CrossCutting.Log.Enum.LogSourceEnum.Api, Infra.CrossCutting.Log.Enum.LogTypeEnum.Navegacao);
                var response = await GenerateUserToken(new LoginModel { UserName = model.Usuario, Password = model.Senha });

                return Response(response);
            }

            AdicionarErrosIdentity(result);
            return Response(model);
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("todos")]
        public async Task<IActionResult> UsuarioTodos()
        {

            List<UsuarioViewModel> userLst = new List<UsuarioViewModel>();

            var usuarios = _usuarioService.ObterTodos();

            foreach (var usuario in usuarios)
            {
                var userVM = _mapper.Map<UsuarioViewModel>(usuario);
                var identityUser = await _userManager.FindByIdAsync(Convert.ToString(usuario.AspNetUserId));

                if (identityUser != null)
                {
                    userVM.Usuario = identityUser.UserName;
                }

                userLst.Add(userVM);
            }

            return Response(userLst);
        }


        //[HttpPost]
        ////[Authorize(Policy = "CanReadUsuario")]
        //[AllowAnonymous]
        //[Route("pesquisa-conta")]
        //public IActionResult Pesquisa([FromBody] PesquisaUsuarioViewModel model, int version)
        //{
        //    if (version == 2)
        //        return Response(new { Message = "API v2 não disponivel" });
        //
        //    if (!ModelState.IsValid)
        //    {
        //        NotificarErroModelInvalida();
        //        return Response(model);
        //    }
        //
        //    //var pesquisaModel = _mapper.Map<PesquisaUsuarioDAO>(model);
        //
        //    //if (Auditar)
        //    //    pesquisaModel.requisicao = HttpContext.TraceIdentifier;
        //
        //    //var data = _usuarioContaService.Pesquisa(pesquisaModel);
        //
        //    //foreach (var item in data.UsuarioLista)
        //    //{
        //    //    // var userIdentity = _userManager.Users.Where(u => u.UsuarioAplicacaoId == item.sIdUsuario).FirstOrDefault();
        //    //
        //    //    //if (userIdentity != null)
        //    //    //{
        //    //    //    item.Login = userIdentity.UserName;
        //    //    //    item.UserFlag = true;
        //    //    //}
        //    //    //else
        //    //    //{
        //    //    //    item.UserFlag = false;
        //    //    //}
        //    //}
        //
        //    //return Response(data);
        //
        //    return Response();
        //}


        [HttpPost]
        [AllowAnonymous]
        [Route("alterar")]
        public async Task<IActionResult> Alterar([FromBody] UsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }

            var appUserOld = _usuarioService.ObterPorId(model.Id);
            var identityUserOld = await _userManager.FindByIdAsync(appUserOld.AspNetUserId.ToString());

            if (identityUserOld != null)
            {
                var updIdentityUser = identityUserOld;
                updIdentityUser.UserName = model.Usuario;
                updIdentityUser.Email = model.Email;

                var result = await _userManager.UpdateAsync(updIdentityUser);

                if (result.Succeeded)
                {
                    var usuarioModel = _mapper.Map<UsuarioModel>(model);
                    _usuarioService.Editar(usuarioModel);

                    if (!OperacaoValida())
                    {
                        await _userManager.UpdateAsync(identityUserOld);
                        return Response(model);
                    }

                    _logService.SaveAudit(HttpContext.TraceIdentifier, string.Format("Usuário {0} alterado com sucesso", model.Nome), JsonConvert.SerializeObject(model), Infra.CrossCutting.Log.Enum.LogSourceEnum.Api, Infra.CrossCutting.Log.Enum.LogTypeEnum.Navegacao);
                    var response = ("Usuário " + model.Nome + " alterado com sucesso!");

                    return Response(response);
                }


                AdicionarErrosIdentity(result);
                return Response(model);
            }

            NotificarErro("2", "Falha ao realizar alteração, usuário não localizado.");
            return Response(model);
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("deletar")]
        public async Task<IActionResult> Desativar([FromBody] Guid usuarioId)
        {
            var usuarioApp = _usuarioService.ObterPorId(usuarioId);

            if (usuarioApp != null)
            {
                var identityUser = await _userManager.FindByIdAsync(usuarioApp.AspNetUserId.ToString());

                if (identityUser != null)
                {
                    identityUser.Active = false;
                    _usuarioService.Remover(usuarioId);

                    if (OperacaoValida())
                    {
                        var result = await _userManager.UpdateAsync(identityUser);

                        if (!result.Succeeded)
                        {
                            AdicionarErrosIdentity(result);
                            return Response(identityUser);
                        }

                        return Response();
                    }

                    NotificarErro("2", "Falha ao deletar usuário.");
                    return Response();
                }

                NotificarErro("2", "Falha ao deletar, usuário não localizado.");
                return Response();
            }

            NotificarErro("2", "Falha ao deletar, usuário não localizado.");
            return Response();
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("esqueci-senha")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model, string appUrl)
        {

            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }

            var user = await _userManager.FindByNameAsync(model.Name);

            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                //var result = await _userManager.ResetPasswordAsync(user, token, "Mudar@123");
                //
                //if (result.Succeeded)
                //{
                //    //TODO: Criar um usuario na base de dados
                //    // var registryCommand = new RegistrySystemUserCommand(Guid.Parse(user.Id), model.Name, model.Email);
                //    // _bus.SendCommand(registryCommand);
                //    // 
                //    // if (!ValidOperation())
                //    // {
                //    //     await _userManager.DeleteAsync(user);
                //    //     return Response(model);
                //    // }
                //
                //
                //    _logger.LogInformation(1, string.Format("Senha do usuário {0} alterado com sucesso!", model.Name));
                //    var response = ("Senha do usuário " + model.Name + " alterado com sucesso!");
                //
                //    return Response(response);
                //}
                //
                //AdicionarErrosIdentity(result);
                //return Response(model);

                // var callbackUrl = Url.Action("ResetPassword", "Account", new { UserId = user.Id, code = token }, protocol: appUrl);
                try
                {
                    var resultMail = _emailSender.SendEmail(user.Email, "Resetar senha",
                    "Prezado usuário, clique nesse link para resetar sua senha: <a href=\"" + string.Format("{0}?Code={1}", appUrl, token) + "\">link</a>");
                }
                catch (Exception ex)
                {
                    NotificarErro(model.Name.ToString(), "Erro ao realizar envio do e-mail.");
                    return Response();
                }

                return Response(string.Format("Foi enviado um e-mail para {0}**@**{1}, com o link para recuperação de senha.", user.Email.Substring(0,3), user.Email.Substring(user.Email.Length-5, 5)));
            }

            NotificarErro(model.Name.ToString(), "Usuário inexistente. Falha ao realizar alteração.");
            return Response(model);
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("reset-senha")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }

            model.Code = model.Code.Replace(" ", "+");

            var user = await _userManager.FindByNameAsync(model.Usuario);

            if (user != null)
            {
                var resultReset = _userManager.ResetPasswordAsync(user, model.Code, model.Password).Result;

                if (!resultReset.Succeeded)
                {
                    NotificarErro("1", "Erro ao resetar a senha.");

                    foreach(var erro in resultReset.Errors)
                    {
                        NotificarErro(erro.Code, erro.Description);
                    }
                }

                return Response();
            }

            NotificarErro(model.Usuario.ToString(), "Usuário inexistente. Falha ao realizar alteração.");
            return Response();
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("nova-senha")]
        public async Task<IActionResult> NewPassword([FromBody] NewPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }

            var user = await _userManager.FindByNameAsync(model.Name);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);


            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

                if (result.Succeeded)
                {
                    //TODO: Criar um usuario na base de dados
                    // var registryCommand = new RegistrySystemUserCommand(Guid.Parse(user.Id), model.Name, model.Email);
                    // _bus.SendCommand(registryCommand);
                    // 
                    // if (!ValidOperation())
                    // {
                    //     await _userManager.DeleteAsync(user);
                    //     return Response(model);
                    // }



                    _logger.LogInformation(1, string.Format("Senha do usuário {0} alterado com sucesso!", model.Name));
                    var response = ("Senha do usuário " + model.Name + " alterado com sucesso!");

                    return Response(response);
                }

                AdicionarErrosIdentity(result);
                return Response(model);
            }

            //NotificarErro(applicationUser.ToString(), "Falha ao realizar alteração.");
            return Response(model);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }

            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null && user.Active)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    _logger.LogInformation(1, string.Format("Usuário {0} logado com sucesso em {1}", model.UserName, DateTime.Now));

                    var response = await GenerateUserToken(model);
                    return Response(response);
                }

                if (_userManager.IsLockedOutAsync(user).Result)
                {
                    NotificarErro("2", "Seu usuário foi bloqueado por 10 minutos, devido ao excesso de tentativas de acesso com erro.");
                }
            }

            NotificarErro("2", "Falha ao realizar o login.");
            return Response(model);
        }


        [HttpGet] // Obter Grupos de Acesso do Usuario
        [Route("{id}/grupos-acesso")]
        //[Authorize(Policy = "CanReadPermissao")]
        [AllowAnonymous]
        public IActionResult UsuarioGruposAcesso(Guid id)
        {
            var usuarioGrupoAcesso = _usuarioService.ObterUsuarioGrupoAcessoPorUsuarioId(id);

            List<GrupoAcessoViewModel> result = new List<GrupoAcessoViewModel>();

            foreach (var grupoAcesso in usuarioGrupoAcesso)
            {
                result.Add(_mapper.Map<GrupoAcessoViewModel>(_grupoAcessoService.ObterPorId(grupoAcesso.GrupoAcessoId)));
            }

            return Response(result.OrderBy(s => s.Nome).ToList());
        }

        [HttpGet] // Obter Grupos de Acesso que podem ser adicionadas no Usuario
        [Route("{id}/grupos-acesso/para-add")]
        //[Authorize(Policy = "CanReadPermissao")]
        [AllowAnonymous]
        public IActionResult UsuarioGrupoAcessoParaAdd(Guid id)
        {
            var menuGrupoAcesso = _usuarioService.ObterUsuarioGrupoAcessoPorUsuarioId(id).Select(e => e.GrupoAcessoId);
            var gruposAcesso = _grupoAcessoService.ObterTodos();
            var gruposAcessoToAdd = _mapper.Map<IEnumerable<GrupoAcessoViewModel>>(gruposAcesso.Where(e => !menuGrupoAcesso.Contains(e.Id)));

            return Response(gruposAcessoToAdd.OrderBy(p => p.Nome));
        }


        [HttpPost] // Adicionar Grupo de Acesso ao Menu
        [Route("grupo-acesso/adicionar")]
        [AllowAnonymous]
        //[Authorize(Policy = "CanWritePermissao")]
        public IActionResult UsuarioGrupoAcessoAdd(UsuarioGrupoAcessoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }

            _usuarioService.AdicionarGrupoAcesso(_mapper.Map<UsuarioGrupoAcesso>(model));

            //if (!OperacaoValida())
            //    return Response(model);
            //
            ////Carrega Usuario App
            //var usuario = _usuarioService.ObterPorId(model.UsuarioId);
            //
            ////Carregar usuario do identity
            //var identityUser = _userManager.FindByIdAsync(Convert.ToString(usuario.AspNetUserId)).Result;
            ////TODO 1: Localizar todas permissoes do grupo de acesso
            //var permissoes = _grupoAcessoService.ObterGrupoAcessoPermissaoPorGrupoId(model.GrupoAcessoId);
            //
            ////Criar uma lista de claims
            ////Adicionar essas claims ao usuario
            //foreach (var permissao in permissoes)
            //{
            //    var permissaoModel = _permissaoService.ObterPorId(permissao.PermissaoId);
            //
            //    Claim newClain = new Claim(permissaoModel.Tipo.Nome, permissaoModel.Valor);
            //
            //    //Verificar se o result é sucesso
            //    var result = _userManager.AddClaimAsync(identityUser, newClain).Result;
            //
            //    if (!result.Succeeded)
            //        AdicionarErrosIdentity(result);
            //}

            return Response();
        }


        [HttpPost] // Remover grupo de acesso do usuario
        [Route("grupo-acesso/remover")]
        [AllowAnonymous]
        //[Authorize(Policy = "CanWritePermissao")]
        public IActionResult UsuarioGrupoAcessoRemover(UsuarioGrupoAcessoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }

            _usuarioService.RemoverUsuarioGrupoAcesso(model.UsuarioId, model.GrupoAcessoId);

            //var gruposDeAcessoUsuario = _usuarioService.ObterUsuarioGrupoAcessoPorUsuarioId(model.UsuarioId);
            //
            //List<PermissaoModel> permissoesManter = new List<PermissaoModel>();
            //foreach(var grupo in gruposDeAcessoUsuario.Where(g => g.GrupoAcessoId != model.GrupoAcessoId))
            //{
            //    var lstGrupoAcessoPermissao = _grupoAcessoService.ObterGrupoAcessoPermissaoPorGrupoId(grupo.GrupoAcessoId);
            //
            //    foreach(var gAPermissao in lstGrupoAcessoPermissao)
            //    {
            //        permissoesManter.Add(_permissaoService.ObterPorId(gAPermissao.PermissaoId));
            //    }
            //}
            //
            //var lstGrupoAcessoPermissaoRemover = _grupoAcessoService.ObterGrupoAcessoPermissaoPorGrupoId(model.GrupoAcessoId);
            //List<PermissaoModel> permissoesRemover = new List<PermissaoModel>();
            //
            //foreach (var gAPermissao in lstGrupoAcessoPermissaoRemover)
            //{
            //    permissoesRemover.Add(_permissaoService.ObterPorId(gAPermissao.PermissaoId));
            //}
            //
            ////Carrega Usuario App
            //var usuario = _usuarioService.ObterPorId(model.UsuarioId);
            //
            ////Carregar usuario do identity
            //var identityUser = _userManager.FindByIdAsync(Convert.ToString(usuario.AspNetUserId)).Result;
            ////TODO 1: Localizar todas permissoes do grupo de acesso
            //var permissoes = _grupoAcessoService.ObterGrupoAcessoPermissaoPorGrupoId(model.GrupoAcessoId);
            //
            ////Criar uma lista de claims
            ////Adicionar essas claims ao usuario
            //foreach (var permissao in permissoesRemover.Where(p => !permissoesManter.Select(m => m.Id).ToList().Contains(p.Id)))
            //{
            //    Claim removeClain = new Claim(permissao.Tipo.Nome, permissao.Valor);
            //
            //    //Verificar se o result é sucesso
            //    var result = _userManager.RemoveClaimAsync(identityUser, removeClain).Result;
            //
            //    if (!result.Succeeded)
            //        AdicionarErrosIdentity(result);
            //}
            
            return Response();
        }


        [HttpGet] // Obter permissoes do usuario logado
        [Route("permissoes")]
        [Authorize]
        public IActionResult PermissoesUsuario()
        {
            var usuarioApp = _usuarioService.ObterPorAspNetUserId(Convert.ToString(UsuarioId));

            var gruposDeAcessoUsuario = _usuarioService.ObterUsuarioGrupoAcessoPorUsuarioId(usuarioApp.Id);

            List<PermissaoModel> permissoesUsuario = new List<PermissaoModel>();

            foreach (var grupo in gruposDeAcessoUsuario)
            {
                var lstGrupoAcessoPermissao = _grupoAcessoService.ObterGrupoAcessoPermissaoPorGrupoId(grupo.GrupoAcessoId);

                foreach (var gAPermissao in lstGrupoAcessoPermissao)
                {
                    permissoesUsuario.Add(_permissaoService.ObterPorId(gAPermissao.PermissaoId));
                }
            }

            var permissaoLstVM = _mapper.Map<List<PermissaoViewModel>>(permissoesUsuario);

            return Response(permissaoLstVM);
        }


        private async Task<object> GenerateUserToken(LoginModel login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            var userClaims = await _userManager.GetClaimsAsync(user);

            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.NameId, user.Id));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.GivenName, user.NormalizedUserName));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, await _jwtTokenOptions.JtiGenerator()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtTokenOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64));

            var claimsUser = CarregarPermissoes(_usuarioService.ObterPorAspNetUserId(user.Id));

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

        private List<Claim> CarregarPermissoes(UsuarioModel usuarioApp)
        {
            // TODO: Criar um metodo no repository usando dapper para carregar isso mais rapido, direto do banco usando joins

            List<Claim> claimsUser = new List<Claim>();

            var gruposDeAcessoUsuario = _usuarioService.ObterUsuarioGrupoAcessoPorUsuarioId(usuarioApp.Id);

            List<PermissaoModel> permissoesUsuario = new List<PermissaoModel>();

            foreach(var grupo in gruposDeAcessoUsuario)
            {
                var lstGrupoAcessoPermissao = _grupoAcessoService.ObterGrupoAcessoPermissaoPorGrupoId(grupo.GrupoAcessoId);
            
                foreach(var gAPermissao in lstGrupoAcessoPermissao)
                {
                    permissoesUsuario.Add(_permissaoService.ObterPorId(gAPermissao.PermissaoId));
                }
            }

            foreach(var permissao in permissoesUsuario)
            {
                claimsUser.Add(new Claim(permissao.Tipo.Nome, permissao.Valor));
            }

            return claimsUser;
        }
    }
}