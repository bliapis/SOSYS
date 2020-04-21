using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.SO.Application.ViewModels.Gerencial.Usuario;

namespace LT.SO.Application.Services.Usuario
{
    public interface IUsuarioAppService
    {
        Task<Guid> Cadastrar(AddUsuarioViewModel usuarioViewModel);
        IEnumerable<UsuarioViewModel> ObterTodos();
        void Atualizar(UsuarioViewModel usuarioViewModel);
        void Desativar(Guid id);
        void ForgotPassword(ForgotPasswordViewModel model);
        void ResetPassword(ResetPasswordViewModel model);
        void NewPassword(NewPasswordViewModel model);
        Task<object> Login(LoginViewModel model);
    }
}