using AutoMapper;
using LT.SO.Domain.Gerencial.Usuario.Entities;
using LT.SO.Application.ViewModels.Gerencial.Usuario;

namespace LT.SO.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            #region Permissao
          //CreateMap<TipoPermissaoModel, TipoPermissaoViewModel>();
          //CreateMap<PermissaoModel, PermissaoViewModel>()
          //    .ForMember(dest => dest.TipoNome, opt => opt.MapFrom(source => source.Tipo.Nome));
            #endregion

            #region Grupo Acesso
          //CreateMap<GrupoAcessoModel, GrupoAcessoViewModel>();
          //CreateMap<GrupoAcessoPermissao, GrupoAcessoPermissaoViewModel>();
            #endregion

            #region Menu
          //CreateMap<MenuModel, MenuViewModel>();
          //CreateMap<MenusGruposAcesso, MenuGrupoAcessoViewModel>();
            #endregion

            #region Usuario
            CreateMap<UsuarioModel, UsuarioViewModel>();
            //CreateMap<UsuarioGrupoAcesso, UsuarioGrupoAcessoViewModel>();
            #endregion
        }
    }
}