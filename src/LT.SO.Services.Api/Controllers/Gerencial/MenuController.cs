using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using LT.SO.Services.Api.Models;
using LT.SO.Services.Api.ViewModels.Gerencial.Menu;
using LT.SO.Services.Api.ViewModels.Gerencial.GrupoAcesso;
using LT.SO.Domain.Core.Bus;
using LT.SO.Domain.Core.Interfaces;
using LT.SO.Domain.Core.Notifications;
using LT.SO.Domain.Permissoes.Menu.Entities;
using LT.SO.Domain.Permissoes.Menu.Interfaces.Service;
using LT.SO.Domain.Permissoes.GrupoAcesso.Interfaces.Service;
using LT.SO.Infra.CrossCutting.Log.Services;

namespace LT.SO.Services.Api.Controllers.Gerencial
{
    [Route("menu/")]
    public class MenuController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogService _logService;
        private readonly IMenuService _menuService;
        private readonly IGrupoAcessoService _grupoAcessoService;

        public MenuController(
            IDomainNotificationHandler<DomainNotification> notifications,
            IUser user,
            IBus bus,
            IOptions<AuditConfig> auditConfig,
            IMapper mapper,
            ILogService logService,
            IMenuService menuService,
            IGrupoAcessoService grupoAcessoService) : base(notifications, user, bus, auditConfig)
        {
            _mapper = mapper;
            _logService = logService;
            _menuService = menuService;
            _grupoAcessoService = grupoAcessoService;
        }

        [HttpGet] // Obter Todos
        [Route("todos")]
        //[Authorize(Policy = "CanReadPermissao")]
        [AllowAnonymous]
        public IActionResult Menus()
        {
            var result = _mapper.Map<IEnumerable<MenuViewModel>>(_menuService.ObterTodos());

            return Response(result);
        }

        [HttpGet] // Obter por Id
        [Route("{id}")]
        //[Authorize(Policy = "CanReadPermissao")]
        [AllowAnonymous]
        public IActionResult Menu(Guid id)
        {
            var result = _mapper.Map<MenuViewModel>(_menuService.ObterPorId(id));

            return Response(result);
        }

        [HttpGet] // Obter Grupos de Acesso do Menu
        [Route("{id}/grupos-acesso")]
        //[Authorize(Policy = "CanReadPermissao")]
        [AllowAnonymous]
        public IActionResult MenuGruposAcesso(Guid id)
        {
            var menuGrupoAcesso = _menuService.ObterGruposAcessoMenu(id);

            List<GrupoAcessoViewModel> result = new List<GrupoAcessoViewModel>();

            foreach (var grupoAcesso in menuGrupoAcesso)
            {
                result.Add(_mapper.Map<GrupoAcessoViewModel>(_grupoAcessoService.ObterPorId(grupoAcesso.GrupoAcessoId)));
            }

            return Response(result.OrderBy(s => s.Nome).ToList());
        }

        [HttpGet] // Obter Grupos de Acesso que podem ser adicionadas no Menu
        [Route("{id}/grupos-acesso/para-add")]
        //[Authorize(Policy = "CanReadPermissao")]
        [AllowAnonymous]
        public IActionResult MenuGrupoAcessoParaAdd(Guid id)
        {
            var menuGrupoAcesso = _menuService.ObterGruposAcessoMenu(id).Select(e => e.GrupoAcessoId);

            var gruposAcesso = _grupoAcessoService.ObterTodos();
            var gruposAcessoToAdd = _mapper.Map<IEnumerable<GrupoAcessoViewModel>>(gruposAcesso.Where(e => !menuGrupoAcesso.Contains(e.Id)));

            return Response(gruposAcessoToAdd.OrderBy(p => p.Nome));
        }

        [HttpGet] // Obter Menus que o usuario tem acesso
        [Route("menus-usuario")]
        //[Authorize(Policy = "CanReadPermissao")]
        [AllowAnonymous]
        public IActionResult MenuGrupoAcessoUsuario()
        {
            var menusUsuario = _mapper.Map<IEnumerable<MenuViewModel>>(_menuService.ObterMenusUsuario(UsuarioId));
        
            return Response(menusUsuario);
        }

        [HttpPost] // Adicionar Menu
        [Route("adicionar")]
        [AllowAnonymous]
        //[Authorize(Policy = "CanWritePermissao")]
        public IActionResult MenuAdd(MenuViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }

            _menuService.Adicionar(_mapper.Map<MenuModel>(model));

            return Response();
        }

        [HttpPost] // Adicionar Grupo de Acesso ao Menu
        [Route("grupo-acesso/adicionar")]
        [AllowAnonymous]
        //[Authorize(Policy = "CanWritePermissao")]
        public IActionResult MenuGrupoAcessoAdd(MenuGrupoAcessoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }

            _menuService.AdicionarGrupoAcesso(_mapper.Map<MenusGruposAcesso>(model));

            return Response();
        }

        [HttpPut] // Editar Menu
        [Route("editar")]
        [AllowAnonymous]
        //[Authorize(Policy = "CanWritePermissao")]
        public IActionResult MenuEdit(MenuViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }

            _menuService.Editar(_mapper.Map<MenuModel>(model));

            return Response();
        }

        [HttpDelete] // Remover Menu
        [Route("remover")]
        [AllowAnonymous]
        //[Authorize(Policy = "CanWritePermissao")] 
        public IActionResult MenuRemove(Guid id)
        {
            _menuService.Remover(id);

            return Response();
        }

        [HttpDelete] // Remover Grupo de Acesso do Menu
        [Route("grupo-acesso/remover")]
        [AllowAnonymous]
        //[Authorize(Policy = "CanWritePermissao")] 
        public IActionResult GrupoAcessoRemovePermissao(Guid menuId, Guid grupoAcessoId)
        {
            _menuService.RemoverGrupoAcesso(menuId, grupoAcessoId);

            return Response();
        }
    }
}