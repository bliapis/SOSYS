using System;
using System.Linq;
using System.Collections.Generic;
using LT.SO.Domain.Gerencial.Service;
using LT.SO.Domain.Permissoes.Menu.Entities;
using LT.SO.Domain.Permissoes.Menu.Interfaces.Service;
using LT.SO.Domain.Permissoes.Menu.Interfaces.Repository;
using LT.SO.Domain.Gerencial.Usuario.Interfaces.Repository;
using LT.SO.Domain.Core.Bus;
using LT.SO.Domain.Core.Interfaces;
using LT.SO.Domain.Core.Notifications;

namespace LT.SO.Domain.Permissoes.Menu.Services
{
    public class MenuService : ServiceBase, IMenuService
    {
        private readonly IBus _bus;
        private readonly IMenuRepository _menuRepo;
        private readonly IUsuarioRepository _usuarioRepo;

        public MenuService(
            IUnitOfWork uow, 
            IBus bus, 
            IDomainNotificationHandler<DomainNotification> notifications,
            IMenuRepository menuRepo,
            IUsuarioRepository usuarioRepo) : base(uow, bus, notifications)
        {
            _bus = bus;
            _menuRepo = menuRepo;
            _usuarioRepo = usuarioRepo;
        }

        public void Adicionar(MenuModel menu)
        {
            if (!ValidMenu(menu)) return;
            if (!ChecarMenuExistente(menu, "2")) return;

            _menuRepo.Add(menu);

            Commit();
        }

        public void AdicionarGrupoAcesso(MenusGruposAcesso menuGrupoAcesso)
        {
            if (ChecarMenuGrupoAcessoExistente(menuGrupoAcesso.MenuId, menuGrupoAcesso.GrupoAcessoId, false))
            {
                _bus.RaiseEvent(new DomainNotification("2", "Esse Grupo de Acesso já faz parte desse Menu."));
                return;
            }

            _menuRepo.AddGrupoAcesso(menuGrupoAcesso);

            Commit();
        }

        public MenuModel ObterPorId(Guid menuId)
        {
            return _menuRepo.GetById(menuId);
        }

        public IEnumerable<MenuModel> ObterTodos()
        {
            var menus = _menuRepo.GetAll();
            return menus;
        }

        public IEnumerable<MenusGruposAcesso> ObterGruposAcessoMenu(Guid menuId)
        {
            var menusGrupoAcesso = _menuRepo.GetMenuGrupoAcessoPorMenuId(menuId);
            return menusGrupoAcesso;
        }

        public void Editar(MenuModel menu)
        {
            if (!ValidMenu(menu)) return;

            _menuRepo.Update(menu);

            Commit();
        }

        public void Remover(Guid menuId)
        {
            if (!ChecarMenuExistente(menuId, "2")) return;

            _menuRepo.Remove(menuId);

            Commit();
        }

        public void RemoverGrupoAcesso(Guid menuId, Guid grupoAcessoId)
        {
            if (!ChecarMenuGrupoAcessoExistente(menuId, grupoAcessoId)) return;

            _menuRepo.RemoveMenuGrupoAcesso(menuId, grupoAcessoId);

            Commit();
        }

        public IEnumerable<MenuModel> ObterMenusSemPai()
        {
            return _menuRepo.Find(m => m.MenuPai == null);
        }

        public IEnumerable<MenuModel> ObterMenusUsuario(Guid usuarioId)
        {
            List<MenuModel> menuLst = new List<MenuModel>();

            var usuarioGrupoAcesso = _usuarioRepo.GetUsuarioGrupoAcesso(usuarioId);

            List<MenusGruposAcesso> menuGrupoAcessoLst = new List<MenusGruposAcesso>();

            foreach(var menuGrupo in usuarioGrupoAcesso)
            {
                menuGrupoAcessoLst.AddRange(_menuRepo.GetMenuGrupoAcessoPorGrupoId(menuGrupo.GrupoAcessoId));
            }

            foreach(var menuGAcesso in menuGrupoAcessoLst)
            {
                menuLst.Add(_menuRepo.GetById(menuGAcesso.MenuId));
            }
            
            return menuLst;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private bool ValidMenu(MenuModel menu)
        {
            if (menu.IsValid()) return true;

            NotifyErrorValidation(menu.ValidationResult);
            return false;
        }

        private bool ChecarMenuExistente(Guid id, string messageType)
        {
            var menu = _menuRepo.GetById(id);

            if (menu != null) return true;

            _bus.RaiseEvent(new DomainNotification(messageType, "Menu não encontrado."));
            return false;
        }

        private bool ChecarMenuExistente(MenuModel menuModel, string messageType)
        {
            var menus = _menuRepo.GetByNomeEPai(menuModel.Nome.ToLower(), menuModel.MenuPaiId);

            if (menus.Count() == 0 || menus.Contains(menuModel)) return true;

            _bus.RaiseEvent(new DomainNotification(messageType, "Já existe um Menu com esse nome."));
            return false;
        }

        private bool ChecarMenuGrupoAcessoExistente(Guid menuId, Guid grupoAcessoId, bool lancaMsg = true)
        {
            var menuGrupoAcesso = _menuRepo.GetMenuGrupoAcesso(menuId, grupoAcessoId);

            if (menuGrupoAcesso != null) return true;
            
            if (lancaMsg)
                _bus.RaiseEvent(new DomainNotification("2", "Esse Grupo de Acesso não foi encontrado nesse Menu."));

            return false;
        }
    }
}