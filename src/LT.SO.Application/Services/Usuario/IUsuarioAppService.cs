using System;
using System.Collections.Generic;
using LT.SO.Application.ViewModels.Gerencial.Usuario;

namespace LT.SO.Application.Services.Usuario
{
    public interface IUsuarioAppService
    {
        void Cadastrar(AddUsuarioViewModel usuarioViewModel);
        IEnumerable<UsuarioViewModel> ObterTodos();
        void Atualizar(UsuarioViewModel usuarioViewModel);
        void Desativar(Guid id);
        void ForgotPassword(ForgotPasswordViewModel model);
        void ResetPassword(ResetPasswordViewModel model);
        void NewPassword(NewPasswordViewModel model);
        LoginViewModel Login(LoginViewModel model);
    }
}