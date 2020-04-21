using AutoMapper;
using LT.SO.Domain.Permissoes.Permissao.Entities;
using LT.SO.Domain.Permissoes.GrupoAcesso.Entities;
using LT.SO.Domain.Permissoes.Menu.Entities;
using LT.SO.Domain.Gerencial.Usuario.Entities;
using LT.SO.Services.Api.ViewModels.Gerencial.Permissoes;
using LT.SO.Services.Api.ViewModels.Gerencial.GrupoAcesso;
using LT.SO.Services.Api.ViewModels.Gerencial.Menu;
using LT.SO.Application.ViewModels.Gerencial.Usuario;

namespace LT.SO.Services.Api.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            #region Permissoes
            CreateMap<TipoPermissaoViewModel, TipoPermissaoModel>();
            CreateMap<PermissaoViewModel, PermissaoModel>();
            CreateMap<AddPermissaoViewModel, PermissaoModel>()
                .ForMember(dest => dest.Tipo, option => option.Ignore())
                .ForMember(dest => dest.CascadeMode, option => option.Ignore())
                .ForMember(dest => dest.Id, option => option.Ignore());
            #endregion

            #region Grupo Acesso
            CreateMap<GrupoAcessoViewModel, GrupoAcessoModel>();
            CreateMap<GrupoAcessoPermissaoViewModel, GrupoAcessoPermissao>();
            #endregion

            #region Menu
            CreateMap<MenuViewModel, MenuModel>();
            CreateMap<MenuGrupoAcessoViewModel, MenusGruposAcesso>();
            #endregion

            #region Usuario
            CreateMap<AddUsuarioViewModel, UsuarioModel>();
            CreateMap<UsuarioViewModel, UsuarioModel>();
            CreateMap<UsuarioGrupoAcessoViewModel, UsuarioGrupoAcesso>();
            #endregion
        }
    }
}