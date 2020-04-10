using System.Threading.Tasks;
using Refit;
using LT.SO.Site.Models;
using LT.SO.Site.Models.Account;

namespace LT.SO.Site.Services
{
    public interface IAccountService
    {
        [Post("/api/v1/usuario/login")]
        Task<ServiceResult> PostLoginAsync(LoginModel request);

        [Get("/api/v1/usuario/todos")]
        Task<ServiceResult> GetUsuarios();

        [Get("/api/v1/usuario/permissoes")]
        [Headers("Authorization: Bearer")]
        Task<ServiceResult> GetPermissoes();

        [Post("/api/v1/usuario/esqueci-senha")]
        Task<ServiceResult> PostForgotPassword(ForgotPasswordModel request, string appUrl);

        [Post("/api/v1/usuario/reset-senha")]
        Task<ServiceResult> PostResetPassword(ResetPasswordModel request);
    }
}