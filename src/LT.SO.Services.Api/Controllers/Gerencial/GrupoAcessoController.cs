using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using AutoMapper;
using LT.SO.Services.Api.Models;
using LT.SO.Services.Api.ViewModels.Gerencial.Permissoes;
using LT.SO.Services.Api.ViewModels.Gerencial.GrupoAcesso;
using LT.SO.Domain.Core.Bus;
using LT.SO.Domain.Core.Interfaces;
using LT.SO.Domain.Core.Notifications;
using LT.SO.Domain.Permissoes.GrupoAcesso.Entities;
using LT.SO.Domain.Permissoes.Permissao.Interfaces.Services;
using LT.SO.Domain.Permissoes.GrupoAcesso.Interfaces.Service;
using LT.SO.Infra.CrossCutting.Log.Interfaces;
using LT.SO.Domain.Gerencial.GrupoAcesso.DTO;

namespace LT.SO.Services.Api.Controllers.Gerencial
{
    [Route("grupo-acesso/")]
    public class GrupoAcessoController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogService _logService;
        private readonly IGrupoAcessoService _grupoAcessoService;
        private readonly IPermissaoService _permissaoService;

        public GrupoAcessoController(
            IDomainNotificationHandler<DomainNotification> notifications,
            IUser user,
            IBus bus,
            IOptions<AuditConfig> auditConfig,
            IMapper mapper,
            ILogService logService,
            IGrupoAcessoService grupoAcessoService,
            IPermissaoService permissaoService) : base(notifications, user, bus, auditConfig)
        {
            _mapper = mapper;
            _logService = logService;
            _grupoAcessoService = grupoAcessoService;
            _permissaoService = permissaoService;
        }

        [HttpGet] // Obter Todos
        [Route("todos")]
        //[Authorize(Policy = "CanReadGrupoAcesso")]
        [AllowAnonymous]
        public IActionResult GrupoAcesso()
        {
            var result = _mapper.Map<IEnumerable<GrupoAcessoViewModel>>(_grupoAcessoService.ObterTodos());

            return Response(result);
        }

        [HttpGet] // Obter por Id
        [Route("{id}")]
        //[Authorize(Policy = "CanReadPermissao")]
        [AllowAnonymous]
        public IActionResult GrupoAcesso(Guid id)
        {
            var result = _mapper.Map<GrupoAcessoViewModel>(_grupoAcessoService.ObterPorId(id));

            return Response(result);
        }

        [HttpGet] // Obter permissoes do grupo de acesso
        [Route("{id}/permissoes")]
        //[Authorize(Policy = "CanReadPermissao")]
        [AllowAnonymous]
        public IActionResult GrupoAcessoPermissoes(Guid id)
        {
            var grupoAcessoPermissoes = _grupoAcessoService.ObterGrupoAcessoPermissaoPorGrupoId(id);

            List<PermissaoViewModel> result = new List<PermissaoViewModel>();

            foreach (var permissao in grupoAcessoPermissoes)
            {
                result.Add(_mapper.Map<PermissaoViewModel>(_permissaoService.ObterPorId(permissao.PermissaoId)));
            }

            return Response(result.OrderBy(s => s.TipoNome).ToList());
        }

        [HttpGet] // Obter Permissoes que podem ser adicionadas no Grupo de Acesso
        [Route("{id}/permissoes/para-add")]
        //[Authorize(Policy = "CanReadPermissao")]
        [AllowAnonymous]
        public IActionResult GrupoAcessoPermissoesParaAdd(Guid id)
        {
            var grupoAcessoPermissoes = _grupoAcessoService.ObterGrupoAcessoPermissaoPorGrupoId(id).Select(e => e.PermissaoId);

            var permissoes = _permissaoService.ObterPorTodos();
            var permissoesToAdd = _mapper.Map<IEnumerable<PermissaoViewModel>>(permissoes.Where(e => !grupoAcessoPermissoes.Contains(e.Id)));

            return Response(permissoesToAdd.OrderBy(p => p.TipoNome));
        }

        [HttpPost] // Obter Paginado
        [Route("pesquisar")]
        //[Authorize(Policy = "CanReadPermissao")]
        [AllowAnonymous]
        public IActionResult PermissaoPesquisar(GrupoAcessoFilter filter)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(filter);
            }

            var result = _grupoAcessoService.ObterPaginado(filter);
            result.LstRetorno = _mapper.Map<List<GrupoAcessoViewModel>>(result.LstRetorno).Cast<object>().ToList();

            return Response(result);
        }

        [HttpPost] // Adicionar Grupo de Acesso
        [Route("adicionar")]
        [AllowAnonymous]
        //[Authorize(Policy = "CanWritePermissao")]
        public IActionResult GrupoAcessoAdd(GrupoAcessoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }

            _grupoAcessoService.Adicionar(_mapper.Map<GrupoAcessoModel>(model));

            return Response();
        }

        [HttpPost] // Adicionar Permissao no Grupo de Acesso
        [Route("adicionar/permissao")]
        [AllowAnonymous]
        //[Authorize(Policy = "CanWritePermissao")]
        public IActionResult GrupoAcessoPermissaoAdd(GrupoAcessoPermissaoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }

            _grupoAcessoService.AdicionarPermissao(_mapper.Map<GrupoAcessoPermissao>(model));

            return Response();
        }

        [HttpPut] // Editar Grupo de Acesso
        [Route("editar")]
        [AllowAnonymous]
        //[Authorize(Policy = "CanWritePermissao")]
        public IActionResult GrupoAcessoEdit(GrupoAcessoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }

            _grupoAcessoService.Editar(_mapper.Map<GrupoAcessoModel>(model));

            return Response();
        }

        [HttpDelete] // Remover Grupo de Acesso
        [Route("remover")]
        [AllowAnonymous]
        //[Authorize(Policy = "CanWritePermissao")] 
        public IActionResult GrupoAcessoRemove(Guid id)
        {
            _grupoAcessoService.Remover(id);

            return Response();
        }

        [HttpDelete] // Remover Permissao do Grupo de Acesso
        [Route("remover/permissao")]
        [AllowAnonymous]
        //[Authorize(Policy = "CanWritePermissao")] 
        public IActionResult GrupoAcessoRemovePermissao(Guid grupoId, Guid permissaoId)
        {
            _grupoAcessoService.RemoverGrupoAcessoPermissao(grupoId, permissaoId);

            return Response();
        }
    }
}