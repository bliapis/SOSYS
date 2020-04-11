using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using LT.SO.Services.Api.Models;
using LT.SO.Services.Api.ViewModels.Gerencial.Permissoes;
using LT.SO.Domain.Core.Bus;
using LT.SO.Domain.Core.Interfaces;
using LT.SO.Domain.Core.Notifications;
using LT.SO.Domain.Gerencial.Permissao.DTO;
using LT.SO.Domain.Permissoes.Permissao.Entities;
using LT.SO.Domain.Permissoes.Permissao.Interfaces.Services;
using LT.SO.Infra.CrossCutting.Log.Services;

namespace LT.SO.Services.Api.Controllers.Gerencial
{
    [ApiController]
    [Route("permissoes/")]
    public class PermissaoController : BaseController
    {
        private readonly ITipoPermissaoService _tipoPermissaoService;
        private readonly IPermissaoService _permissaoService;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;

        public PermissaoController(
            IBus bus,
            IUser user,
            IMapper mapper, 
            IOptions<AuditConfig> auditConfig,
            IDomainNotificationHandler<DomainNotification> notifications,
            ILogService logService,
            ITipoPermissaoService tipoPermissaoService,
            IPermissaoService permissaoService) : base(notifications, user, bus, auditConfig)
        {
            _tipoPermissaoService = tipoPermissaoService;
            _permissaoService = permissaoService;
            _mapper = mapper;
            _logService = logService;
        }

        #region Tipo Permissao

        [HttpGet] // Obter Todos
        [Route("tipo-permissao/todos")]
        //[Authorize(Policy = "CanReadPermissao")]
        [AllowAnonymous]
        public IActionResult TipoPermissao()
        {
            var result = _mapper.Map<List<TipoPermissaoViewModel>>(_tipoPermissaoService.ObterTodos());

            return Response(result);
        }

        [HttpGet] // Obter por Id
        [Route("tipo-permissao/{id}")]
        //[Authorize(Policy = "CanReadPermissao")]
        public IActionResult TipoPermissao(Guid id)
        {
            var result = _mapper.Map<TipoPermissaoViewModel>(_tipoPermissaoService.ObterPorId(id));

            return Response(result);
        }

        [HttpPost] // Obter Paginado
        [Route("tipo-permissao/pesquisar")]
        //[Authorize(Policy = "CanReadPermissao")]
        [AllowAnonymous]
        public IActionResult TipoPermissaoPesquisar(TipoPermissaoFilter filter)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(filter);
            }

            var result = _tipoPermissaoService.ObterPaginado(filter);

            //Preciso fazer isso, pois minhas models são set private. Caso não tire as entities para viewmodels, não conseguirá transformar em json.
            result.LstRetorno = _mapper.Map<List<TipoPermissaoViewModel>>(result.LstRetorno).Cast<object>().ToList();

            return Response(result);
        }

        [HttpPost] // Adicionar
        [Route("tipo-permissao/adicionar")]
        [AllowAnonymous]
        //[Authorize(Policy = "CanWritePermissao")]
        public IActionResult TipoPermissaoAdd(TipoPermissaoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }

            _tipoPermissaoService.Adicionar(_mapper.Map<TipoPermissaoModel>(model));

            return Response();
        }

        [HttpPut] // Editar
        [Route("tipo-permissao/editar")]
        [AllowAnonymous]
        //[Authorize(Policy = "CanWritePermissao")]
        public IActionResult TipoPermissaoEdit(TipoPermissaoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }

            _tipoPermissaoService.Editar(_mapper.Map<TipoPermissaoModel>(model));

            return Response();
        }

        [HttpDelete] // Remover
        [Route("tipo-permissao/remover/{id}")]
        [AllowAnonymous]
        //[Authorize(Policy = "CanWritePermissao")] 
        public IActionResult TipoPermissaoRemove(Guid id)
        {
            _tipoPermissaoService.Remover(id);

            return Response();
        }

        #endregion

        #region Permissão

        [HttpGet] // Obter Todos
        [Route("permissao/todos")]
        //[Authorize(Policy = "CanReadPermissao")]
        [AllowAnonymous]
        public IActionResult Permissao()
        {
            var result = _mapper.Map<IEnumerable<PermissaoViewModel>>(_permissaoService.ObterPorTodos());

            return Response(result);
        }

        [HttpGet] // Obter por Id
        [Route("permissao/{id}")]
        //[Authorize(Policy = "CanReadPermissao")]
        [AllowAnonymous]
        public IActionResult Permissao(Guid id)
        {
            var result = _mapper.Map<PermissaoViewModel>(_permissaoService.ObterPorId(id));

            return Response(result);
        }

        [HttpGet] // Obter Todos Por Tipo Permissao
        [Route("permissao/tipo-permissao/{tipoId}/permissoes")]
        //[Authorize(Policy = "CanReadPermissao")]
        [AllowAnonymous]
        public IActionResult PermissaoTipo(Guid tipoId)
        {
            var result = _mapper.Map<IEnumerable<PermissaoViewModel>>(_permissaoService.ObterPorTipo(tipoId));

            return Response(result);
        }

        [HttpPost] // Obter Paginado
        [Route("permissao/pesquisar")]
        //[Authorize(Policy = "CanReadPermissao")]
        [AllowAnonymous]
        public IActionResult PermissaoPesquisar(PermissaoFilter filter)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(filter);
            }

            var result = _permissaoService.ObterPaginado(filter);
            result.LstRetorno = _mapper.Map<List<PermissaoViewModel>>(result.LstRetorno).Cast<object>().ToList();

            return Response(result);
        }

        [HttpPost] // Adicionar
        [Route("permissao/adicionar")]
        [AllowAnonymous]
        //[Authorize(Policy = "CanWritePermissao")]
        public IActionResult PermissaoAdd(AddPermissaoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }

            _permissaoService.Adicionar(_mapper.Map<PermissaoModel>(model));

            return Response();
        }

        [HttpPut] // Editar
        [Route("permissao/editar")]
        [AllowAnonymous]
        //[Authorize(Policy = "CanWritePermissao")]
        public IActionResult PermissaoEdit(PermissaoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }

            _permissaoService.Editar(_mapper.Map<PermissaoModel>(model));

            return Response();
        }

        [HttpDelete] // Remover
        [Route("permissao/remover/{id}")]
        [AllowAnonymous]
        //[Authorize(Policy = "CanWritePermissao")] 
        public IActionResult PermissaoRemove(Guid id)
        {
            _permissaoService.Remover(id);

            return Response();
        }

        #endregion
    }
}