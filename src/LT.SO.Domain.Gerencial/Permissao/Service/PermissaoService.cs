using System;
using System.Linq;
using System.Collections.Generic;
using LT.SO.Domain.Core.Bus;
using LT.SO.Domain.Core.Models;
using LT.SO.Domain.Core.Interfaces;
using LT.SO.Domain.Core.Notifications;
using LT.SO.Domain.Gerencial.Service;
using LT.SO.Domain.Permissoes.Permissao.Entities;
using LT.SO.Domain.Permissoes.Permissao.Interfaces.Services;
using LT.SO.Domain.Permissoes.Permissao.Interfaces.Repositories;
using LT.SO.Domain.Gerencial.Permissao.DTO;

namespace LT.SO.Domain.Permissoes.Permissao.Service
{
    public class PermissaoService : ServiceBase, IPermissaoService
    {
        private readonly IBus _bus;
        private readonly IPermissaoRepository _permissaoRepo;
        private readonly ITipoPermissaoRepository _tipoPermissaoRepo;

        public PermissaoService(
            IUnitOfWork uow, 
            IBus bus, 
            IDomainNotificationHandler<DomainNotification> notifications,
            IPermissaoRepository permissaoRepo,
            ITipoPermissaoRepository tipoPermissaoRepo) : base(uow, bus, notifications)
        {
            _bus = bus;
            _permissaoRepo = permissaoRepo;
            _tipoPermissaoRepo = tipoPermissaoRepo;
        }

        public PermissaoModel ObterPorId(Guid permissaoId)
        {
            var permissao = _permissaoRepo.GetById(permissaoId);

            permissao.SetTipoPermissao(_tipoPermissaoRepo.GetById(permissao.TipoId));

            return permissao;
        }

        public IEnumerable<PermissaoModel> ObterPorTodos()
        {
            var permissoes = _permissaoRepo.GetAll();
            
            foreach(var permissao in permissoes)
            {
                permissao.SetTipoPermissao(_tipoPermissaoRepo.GetById(permissao.TipoId));
            }

            return permissoes;
        }

        public IEnumerable<PermissaoModel> ObterPorTipo(Guid tipoId)
        {
            var permissoes = _permissaoRepo.Find(p => p.TipoId == tipoId).ToList();

            foreach (var permissao in permissoes)
            {
                permissao.SetTipoPermissao(_tipoPermissaoRepo.GetById(permissao.TipoId));
            }

            return permissoes;
        }

        public DataResult ObterPaginado(PermissaoFilter filter)
        {
            return _permissaoRepo.GetPaginado(filter);
        }

        public void Adicionar(PermissaoModel permissao)
        {
            if (!ValidaPermissao(permissao)) return;

            if (!ChecaPermissaoExistente(permissao, "1")) return;

            _permissaoRepo.Add(permissao);

            Commit();
        }

        public void Editar(PermissaoModel permissao)
        {
            if (!ValidaPermissao(permissao)) return;

            if (!ChecaPermissaoExistente(permissao, "1")) return;

            _permissaoRepo.Update(permissao);

            Commit();
        }

        public void Remover(Guid permissaoId)
        {
            if (!ChecaPermissaoExistente(permissaoId, "2")) return;

            _permissaoRepo.Remove(permissaoId);

            Commit();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private bool ValidaPermissao(PermissaoModel permissao)
        {
            if (permissao.IsValid()) return true;

            NotifyErrorValidation(permissao.ValidationResult);
            return false;
        }

        private bool ChecaPermissaoExistente(Guid id, string messageType)
        {
            var tipoPermissao = _permissaoRepo.GetById(id);

            if (tipoPermissao != null) return true;

            _bus.RaiseEvent(new DomainNotification(messageType, "Permissão não encontrada."));
            return false;
        }

        private bool ChecaPermissaoExistente(PermissaoModel permissaoModel, string messageType)
        {
            var permissao = _permissaoRepo.GetByTipoValor(permissaoModel.TipoId, permissaoModel.Valor);

            if (permissao == null || permissao == permissaoModel) return true;

            _bus.RaiseEvent(new DomainNotification(messageType, "Já existe uma Permissão com esse valor."));
            return false;
        }
    }
}