using System;
using System.Collections.Generic;
using LT.SO.Domain.Gerencial.Service;
using LT.SO.Domain.Gerencial.Usuario.Entities;
using LT.SO.Domain.Gerencial.Usuario.Interfaces.Service;
using LT.SO.Domain.Gerencial.Usuario.Interfaces.Repository;
using LT.SO.Domain.Core.Bus;
using LT.SO.Domain.Core.Interfaces;
using LT.SO.Domain.Core.Notifications;

namespace LT.SO.Domain.Gerencial.Usuario.Services
{
    public class UsuarioService : ServiceBase, IUsuarioService
    {
        private readonly IBus _bus;
        private readonly IUsuarioRepository _usuarioRepo;

        public UsuarioService(
            IUnitOfWork uow,
            IBus bus,
            IDomainNotificationHandler<DomainNotification> notifications,
            IUsuarioRepository usuarioRepo) : base(uow, bus, notifications)
        {
            _bus = bus;
            _usuarioRepo = usuarioRepo;
        }

        public void Adicionar(UsuarioModel userModel)
        {
            var usuario = new UsuarioModel(userModel.Nome, userModel.CPF, userModel.Email, userModel.AspNetUserId);
            if (!ValidarUsuario(usuario)) return;
            if (!ChecarUsuarioExistente(usuario, "2")) return;

            _usuarioRepo.Add(usuario);

            Commit();
        }

        public void AdicionarGrupoAcesso(UsuarioGrupoAcesso usuarioGrupoAcesso)
        {
            if (ChecarUsuarioGrupoAcessoExistente(usuarioGrupoAcesso.UsuarioId, usuarioGrupoAcesso.GrupoAcessoId, false))
            {
                _bus.RaiseEvent(new DomainNotification("2", "Esse usuário já contém esse grupo de acesso."));
                return;
            }


            _usuarioRepo.AddUsuarioGrupoAcesso(usuarioGrupoAcesso);

            Commit();
        }

        public void Editar(UsuarioModel usuario)
        {
            if (!ValidarUsuario(usuario)) return;

            _usuarioRepo.Update(usuario);

            Commit();
        }

        public UsuarioModel ObterPorId(Guid usuarioId)
        {
            return _usuarioRepo.GetById(usuarioId);
        }

        public UsuarioModel ObterPorAspNetUserId(string aspNetUserId)
        {
            return _usuarioRepo.GetByAspNetUserId(Guid.Parse(aspNetUserId));
        }

        public IEnumerable<UsuarioModel> ObterTodos()
        {
            var grupos = _usuarioRepo.GetAll();
            return grupos;
        }

        public IEnumerable<UsuarioGrupoAcesso> ObterUsuarioGrupoAcessoPorUsuarioId(Guid usuarioId)
        {
            var usuarios = _usuarioRepo.GetUsuarioGrupoAcesso(usuarioId);
            return usuarios;
        }

        public void Remover(Guid usuarioId)
        {
            if (!ChecarUsuarioExistente(usuarioId, "2")) return;

            var usuario = _usuarioRepo.GetById(usuarioId);

            usuario.DesativarUsuario();

            Editar(usuario);
        }

        public void RemoverUsuarioGrupoAcesso(Guid usuarioId, Guid grupoAcessoId)
        {
            if (!ChecarUsuarioGrupoAcessoExistente(usuarioId, grupoAcessoId)) return;

            _usuarioRepo.RemoveUsuarioGrupoAcesso(usuarioId, grupoAcessoId);

            Commit();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private bool ValidarUsuario(UsuarioModel usuario)
        {
            if (usuario.IsValid()) return true;

            NotifyErrorValidation(usuario.ValidationResult);
            return false;
        }

        private bool ChecarUsuarioExistente(Guid id, string messageType)
        {
            var usuario = _usuarioRepo.GetById(id);

            if (usuario != null) return true;

            _bus.RaiseEvent(new DomainNotification(messageType, "Usuário não encontrado."));
            return false;
        }

        private bool ChecarUsuarioExistente(UsuarioModel usuarioModel, string messageType)
        {
            var usuario = _usuarioRepo.GetByCpf(usuarioModel.CPF);

            if (usuario == null || usuario == usuarioModel) return true;

            _bus.RaiseEvent(new DomainNotification(messageType, "Já existe um usuário com esse cpf."));
            return false;
        }

        private bool ChecarUsuarioGrupoAcessoExistente(Guid usuarioId, Guid grupoAcessoId, bool lancaMsg = true)
        {
            var usuarioGrupoAcesso = _usuarioRepo.GetUsuarioGrupoAcesso(usuarioId, grupoAcessoId);

            if (usuarioGrupoAcesso != null) return true;

            if (lancaMsg)
                _bus.RaiseEvent(new DomainNotification("2", "Esse Grupo de Acesso não foi encontrado nesse Usuário."));

            return false;
        }
    }
}