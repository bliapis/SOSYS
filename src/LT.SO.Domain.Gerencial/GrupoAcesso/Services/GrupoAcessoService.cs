using System;
using System.Collections.Generic;
using LT.SO.Domain.Core.Bus;
using LT.SO.Domain.Core.Interfaces;
using LT.SO.Domain.Core.Notifications;
using LT.SO.Domain.Gerencial.Service;
using LT.SO.Domain.Permissoes.GrupoAcesso.Entities;
using LT.SO.Domain.Permissoes.GrupoAcesso.Interfaces.Service;
using LT.SO.Domain.Permissoes.GrupoAcesso.Interfaces.Repository;
using LT.SO.Domain.Core.Models;
using LT.SO.Domain.Gerencial.GrupoAcesso.DTO;

namespace LT.SO.Domain.Permissoes.GrupoAcesso.Services
{
    public class GrupoAcessoService : ServiceBase, IGrupoAcessoService
    {
        private readonly IBus _bus;
        private readonly IGrupoAcessoRepository _grupoRepo;

        public GrupoAcessoService(
            IUnitOfWork uow,
            IBus bus,
            IDomainNotificationHandler<DomainNotification> notifications,
            IGrupoAcessoRepository grupoRepo) : base(uow, bus, notifications)
        {
            _bus = bus;
            _grupoRepo = grupoRepo;
        }

        public void Adicionar(GrupoAcessoModel grupoAcesso)
        {
            if (!ValidarGrupoAcesso(grupoAcesso)) return;
            if (!ChecarGrupoAcessoExistente(grupoAcesso, "2")) return;

            _grupoRepo.Add(grupoAcesso);

            Commit();
        }

        public void AdicionarPermissao(GrupoAcessoPermissao grupoAcessoPermissao)
        {
            if (ChecarGrupoAcessoPermissaoExistente(grupoAcessoPermissao.GrupoAcessoId, grupoAcessoPermissao.PermissaoId, false))
            {
                _bus.RaiseEvent(new DomainNotification("2", "Essa Permissão já faz parte desse Grupo de Acesso."));
                return;
            }

            _grupoRepo.AddPermissao(grupoAcessoPermissao);

            Commit();
        }

        public void Editar(GrupoAcessoModel grupoAcesso)
        {
            if (!ValidarGrupoAcesso(grupoAcesso)) return;

            _grupoRepo.Update(grupoAcesso);

            Commit();
        }

        public GrupoAcessoModel ObterPorId(Guid grupoAcessoId)
        {
            return _grupoRepo.GetById(grupoAcessoId);
        }

        public IEnumerable<GrupoAcessoModel> ObterTodos()
        {
            var grupos = _grupoRepo.GetAll();
            return grupos;
        }

        public IEnumerable<GrupoAcessoPermissao> ObterGrupoAcessoPermissaoPorGrupoId(Guid grupoId)
        {
            var grupos = _grupoRepo.GetGrupoAcessoPermissaoPorGrupoId(grupoId);
            return grupos;
        }

        public DataResult ObterPaginado(GrupoAcessoFilter filter)
        {
            return _grupoRepo.GetPaginado(filter);
        }

        public void Remover(Guid grupoAcessoId)
        {
            if (!ChecarGrupoAcessoExistente(grupoAcessoId, "2")) return;

            _grupoRepo.Remove(grupoAcessoId);

            Commit();
        }

        public void RemoverGrupoAcessoPermissao(Guid grupoId, Guid permissaoId)
        {
            if (!ChecarGrupoAcessoPermissaoExistente(grupoId, permissaoId)) return;

            _grupoRepo.RemovePermissaoGrupoAcesso(grupoId, permissaoId);

            Commit();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private bool ValidarGrupoAcesso(GrupoAcessoModel grupoAcesso)
        {
            if (grupoAcesso.IsValid()) return true;

            NotifyErrorValidation(grupoAcesso.ValidationResult);
            return false;
        }

        private bool ChecarGrupoAcessoExistente(Guid id, string messageType)
        {
            var grupoAcesso = _grupoRepo.GetById(id);

            if (grupoAcesso != null) return true;

            _bus.RaiseEvent(new DomainNotification(messageType, "Grupo de Acesso não encontrado."));
            return false;
        }

        private bool ChecarGrupoAcessoExistente(GrupoAcessoModel grupoAcesso, string messageType)
        {
            var grupo = _grupoRepo.GetByNome(grupoAcesso.Nome.ToLower());

            if (grupo == null || grupo == grupoAcesso) return true;

            _bus.RaiseEvent(new DomainNotification(messageType, "Já existe um Grupo de Acesso com esse nome."));
            return false;
        }

        private bool ChecarGrupoAcessoPermissaoExistente(Guid grupoId, Guid permissaoId, bool lancaMsg = true)
        {
            var grupoAcesso = _grupoRepo.GetGrupoAcessoPermissao(grupoId, permissaoId);

            if (grupoAcesso != null) return true;

            if (lancaMsg)
                _bus.RaiseEvent(new DomainNotification("2", "Essa Permissão não foi encontrada nesse Grupo de Acesso."));

            return false;
        }
    }
}