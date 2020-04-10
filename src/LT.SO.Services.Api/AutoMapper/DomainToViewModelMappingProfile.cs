using AutoMapper;
using LT.SO.Domain.Permissoes.Permissao.Entities;
using LT.SO.Services.Api.ViewModels.Gerencial.Permissoes;
using LT.SO.Domain.Permissoes.GrupoAcesso.Entities;
using LT.SO.Services.Api.ViewModels.Gerencial.GrupoAcesso;
using LT.SO.Domain.Permissoes.Menu.Entities;
using LT.SO.Services.Api.ViewModels.Gerencial.Menu;
using LT.SO.Domain.Gerencial.Usuario.Entities;
using LT.SO.Services.Api.ViewModels.Gerencial.Usuario;

namespace LT.SO.Services.Api.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            #region Permissao
            CreateMap<TipoPermissaoModel, TipoPermissaoViewModel>();
            CreateMap<PermissaoModel, PermissaoViewModel>()
                .ForMember(dest => dest.TipoNome, opt => opt.MapFrom(source => source.Tipo.Nome));
            #endregion

            #region Grupo Acesso
            CreateMap<GrupoAcessoModel, GrupoAcessoViewModel>();
            CreateMap<GrupoAcessoPermissao, GrupoAcessoPermissaoViewModel>();
            #endregion

            #region Menu
            CreateMap<MenuModel, MenuViewModel>();
            CreateMap<MenusGruposAcesso, MenuGrupoAcessoViewModel>();
            #endregion

            #region Usuario
            CreateMap<UsuarioModel, UsuarioViewModel>();
            CreateMap<UsuarioGrupoAcesso, UsuarioGrupoAcessoViewModel>();
            #endregion
        }
    }
}