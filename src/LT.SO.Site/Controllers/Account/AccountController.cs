using System;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Refit;
using Newtonsoft.Json;
using LT.SO.Site.Models;
using LT.SO.Site.Models.Enum;
using LT.SO.Site.Models.Account;
using LT.SO.Site.Models.Gerencial.Permissao;
using LT.SO.Site.Services;
using LT.SO.Site.Utils;

namespace LT.SO.Site.Controllers.Account
{
    public class AccountController : BaseController
    {
        private IConfiguration _configuration;

        public AccountController(
            IConfiguration configuration,
            IHostingEnvironment env) : base(configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View("~/Views/Account/Index.cshtml");
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View("~/Views/Account/Index.cshtml");
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Logar(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Account/Index.cshtml", loginModel);
            }

            #region Faz login
            var callResult = ServiceApi.Call(_configuration, HttpContext, "usuario/login", ServiceType.POST, loginModel);

            if (!callResult.Success)
            {
                ViewBag.ErrorMsgs = callResult.Erros;
                return View("~/Views/Account/Index.cshtml", loginModel);
            }

            var resultData = JsonConvert.DeserializeObject<DataLogin>(Convert.ToString(callResult.Data));

            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, resultData.Usuario.Usuario),
                        new Claim(ClaimTypes.Email, resultData.Usuario.Email),
                        new Claim("url", string.Format("{0}://{1}", HttpContext.Request.Scheme, HttpContext.Request.Host)),
                        new Claim("Token", resultData.Token)
                    };

            #endregion

            #region Get Permissoes
            callResult = ServiceApi.Call(_configuration, HttpContext, "usuario/permissoes", ServiceType.GET, null, false, resultData.Token);

            if (!callResult.Success)
            {
                ViewBag.ErrorMsgs = callResult.Erros;
                return View("~/Views/Account/Index.cshtml", loginModel);
            }

            var userPermissoes = JsonConvert.DeserializeObject<List<PermissaoViewModel>>(Convert.ToString(callResult.Data));

            foreach (var permissao in userPermissoes)
            {
                claims.Add(new Claim(permissao.TipoNome, permissao.Valor));
            }

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                ExpiresUtc = resultData.Validade,
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. Required when setting the 
                // ExpireTimeSpan option of CookieAuthenticationOptions 
                // set with AddCookie. Also required when setting 
                // ExpiresUtc.

                IssuedUtc = DateTimeOffset.UtcNow,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };
            #endregion

            this.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties).Wait();

            if (resultData.Usuario.FirstPass)
                return RedirectToAction("TrocarSenha", "Account");

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword(ServiceResult result)
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult RecuperarSenha(ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Account/ForgotPassword.cshtml", model);
            }

            var accountService = RestService.For<IAccountService>(ApiAddress);
            ServiceResult result;

            try
            {
                string url = string.Format("{0}://{1}/Account/ResetPassword", HttpContext.Request.Scheme, HttpContext.Request.Host);

                result = accountService.PostForgotPassword(model, url).Result;

                var resultVB = new ValidationMessage(result);
                resultVB.CallBackUrl = "/Account/Index";
                ViewBag.Result = resultVB;

                return View("~/Views/Account/ForgotPassword.cshtml");
                
            }
            catch (Exception ex)
            {
                result = new ServiceResult();
                result.Success = false;
                result.Erros.Add("Erro ao tentar resetar a senha, aguarde alguns instantes ou contate nosso suporte.");

                ViewBag.Result = new ValidationMessage(result);
                return View("~/Views/Account/ForgotPassword.cshtml");
            }
        }

        [AllowAnonymous]
        public IActionResult ResetPassword(string code)
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ResetarSenha(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var accountService = RestService.For<IAccountService>(ApiAddress);
            ServiceResult result;

            try
            {
                string url = string.Format("{0}://{1}", HttpContext.Request.Scheme, HttpContext.Request.Host);

                result = accountService.PostResetPassword(model).Result;

                var resultVB = new ValidationMessage(result, "Senha alterada com sucesso!");
                resultVB.CallBackUrl = "/Account/Index";
                ViewBag.Result = resultVB;

                //if (!result.Success)
                //    return View("~/Views/Account/Index.cshtml");
                //else
                    return View("~/Views/Account/ResetPassword.cshtml", model);
            }
            catch (Exception ex)
            {
                result = new ServiceResult();
                result.Success = false;
                result.Erros.Add("Erro ao tentar resetar a senha, aguarde alguns instantes ou contate nosso suporte.");

                ViewBag.Result = new ValidationMessage(result);
                return View("~/Views/Account/ForgotPassword.cshtml");
            }
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult AccessDenied()
        {
            ViewData["Message"] = "Desculpe, você não tem permissão para acessar essa funcionalidade.";
            return View("~/Views/Account/AccessDenied.cshtml");
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return View("~/Views/Account/Index.cshtml");
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        public IActionResult TodosUsuarios()
        {
            var accountService = RestService.For<IAccountService>("http://localhost:51336");

            var serviceResult = accountService.GetUsuarios().Result;

            var usuarios = JsonConvert.DeserializeObject<List<UsuarioModel>>(Convert.ToString(serviceResult.Data));

            return View(usuarios);
        }
    }
}