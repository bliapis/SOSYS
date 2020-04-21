using AutoMapper;
using LT.SO.Application.ViewModels.Gerencial.Usuario;
using LT.SO.Domain.Gerencial.Usuario.Commands;
using LT.SO.Domain.Gerencial.Usuario.Entities;

namespace LT.SO.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            #region Permissoes
          //CreateMap<TipoPermissaoViewModel, TipoPermissaoModel>();
          //CreateMap<PermissaoViewModel, PermissaoModel>();
          //CreateMap<AddPermissaoViewModel, PermissaoModel>()
          //    .ForMember(dest => dest.Tipo, option => option.Ignore())
          //    .ForMember(dest => dest.CascadeMode, option => option.Ignore())
          //    .ForMember(dest => dest.Id, option => option.Ignore());
            #endregion

            #region Grupo Acesso
          //CreateMap<GrupoAcessoViewModel, GrupoAcessoModel>();
          //CreateMap<GrupoAcessoPermissaoViewModel, GrupoAcessoPermissao>();
            #endregion

            #region Menu
          //CreateMap<MenuViewModel, MenuModel>();
          //CreateMap<MenuGrupoAcessoViewModel, MenusGruposAcesso>();
            #endregion

            #region Usuario
            CreateMap<AddUsuarioViewModel, UsuarioModel>();
            CreateMap<UsuarioViewModel, UsuarioModel>();
            //CreateMap<UsuarioGrupoAcessoViewModel, UsuarioGrupoAcesso>();

            //Commands
            CreateMap<AddUsuarioViewModel, CreateUsuarioCommand>()
                .ConstructUsing(c => new CreateUsuarioCommand(c.Usuario, c.Nome, c.CPF, c.Email, c.Senha));
            #endregion
        }
    }
}