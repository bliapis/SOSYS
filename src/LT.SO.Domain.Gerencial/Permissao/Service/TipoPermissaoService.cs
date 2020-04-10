using System;
using System.Collections.Generic;
using LT.SO.Domain.Core.Bus;
using LT.SO.Domain.Core.Interfaces;
using LT.SO.Domain.Core.Notifications;
using LT.SO.Domain.Gerencial.Service;
using LT.SO.Domain.Permissoes.Permissao.Entities;
using LT.SO.Domain.Permissoes.Permissao.Interfaces.Services;
using LT.SO.Domain.Permissoes.Permissao.Interfaces.Repositories;
using System.Linq;
using LT.SO.Domain.Core.Models;
using LT.SO.Domain.Gerencial.Permissao.DTO;

namespace LT.SO.Domain.Permissoes.Permissao.Service
{
    public class TipoPermissaoService : ServiceBase, ITipoPermissaoService
    {
        private readonly IBus _bus;
        private readonly ITipoPermissaoRepository _tipoPRepo;
        private readonly IPermissaoRepository _permissaoRepository;
        
        public TipoPermissaoService(
            IUnitOfWork uow,
            IBus bus, 
            IDomainNotificationHandler<DomainNotification> notifications,
            ITipoPermissaoRepository tipoPermissaoRepository,
            IPermissaoRepository permissaoRepository) : base(uow, bus, notifications)
        {
            _bus = bus;
            _tipoPRepo = tipoPermissaoRepository;
            _permissaoRepository = permissaoRepository;
        }

        public void Adicionar(TipoPermissaoModel tipoPermissao)
        {
            if (!ValidaTipoPermissao(tipoPermissao)) return;

            if (!ChecaTipoPermissaoExistente(tipoPermissao, "1")) return;

            _tipoPRepo.Add(tipoPermissao);

            Commit();
        }

        public void Editar(TipoPermissaoModel tipoPermissao)
        {
            if (!ValidaTipoPermissao(tipoPermissao)) return;
            if (!ChecaTipoPermissaoExistente(tipoPermissao, "1")) return;

            _tipoPRepo.Update(tipoPermissao);

            Commit();
        }

        public TipoPermissaoModel ObterPorId(Guid tipoPermissaoId)
        {
            return _tipoPRepo.GetById(tipoPermissaoId);
        }

        public IEnumerable<TipoPermissaoModel> ObterTodos()
        {
            return _tipoPRepo.GetAll();
        }

        public DataResult ObterPaginado(TipoPermissaoFilter filter)
        {
            return _tipoPRepo.GetPaginado(filter);
        }

        public void Remover(Guid tipoPermissaoId)
        {
            if (!ChecaTipoPermissaoExistente(tipoPermissaoId, "1")) return;

            var permissoes = _permissaoRepository.GetByTipo(tipoPermissaoId);

            if (permissoes.Count() > 0)
            {
                _bus.RaiseEvent(new DomainNotification("1", string.Format("Não é possível deletar esse Tipo de Permissão, pois existem {0} permissões atreladas.", permissoes.Count())));
                return;
            }

            _tipoPRepo.Remove(tipoPermissaoId);

            Commit();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private bool ValidaTipoPermissao(TipoPermissaoModel tipoPermissao)
        {
            if (tipoPermissao.IsValid()) return true;
        
            NotifyErrorValidation(tipoPermissao.ValidationResult);
            return false;
        }

        private bool ChecaTipoPermissaoExistente(Guid id, string messageType)
        {
            var tipoPermissao = _tipoPRepo.GetById(id);

            if (tipoPermissao != null) return true;

            _bus.RaiseEvent(new DomainNotification(messageType, "Tipo de Permissão não encontrado."));
            return false;
        }

        private bool ChecaTipoPermissaoExistente(TipoPermissaoModel tipoPermissao, string messageType)
        {
            var tipo = _tipoPRepo.GetByNome(tipoPermissao.Nome.ToLower());

            if (tipo == null || tipo == tipoPermissao) return true;

            _bus.RaiseEvent(new DomainNotification(messageType, "Já existe um Tipo de Permissão com esse nome."));
            return false;
        }
    }
}