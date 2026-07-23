using AutoMapper;
using Florama.Api.Request.Auth;
using Florama.Api.Request.Categoria;
using Florama.Api.Request.Estoque;
using Florama.Api.Request.Produto;
using Florama.Api.Request.Usuario;
using Florama.Api.ViewModel;
using Florama.Api.ViewModel.Auth;
using Florama.Api.ViewModel.Categoria;
using Florama.Api.ViewModel.Estoque;
using Florama.Api.ViewModel.Produto;
using Florama.Api.ViewModel.Usuario;
using Florama.Application.DTOs;
using Florama.Application.DTOs.Auth;

namespace Florama.Api.AutoMapper
{
    public class PresentationMappingProfile : Profile
    {
        public PresentationMappingProfile()
        {
            //CATEGORIA
            CreateMap<CategoriaDto, CategoriaViewModel>().ReverseMap();
            CreateMap<CategoriaDto, CriarCategoriaRequest>().ReverseMap();
            CreateMap<CategoriaDto, AtualizarCategoriaRequest>().ReverseMap();

            //PRODUTO
            CreateMap<ProdutoFotoDto, ProdutoFotoViewModel>().ReverseMap();
            CreateMap<AdicionarFotoRequest, ProdutoFotoDto>();
            CreateMap<ProdutoDto, ProdutoViewModel>()
                .ForMember(dest => dest.Fotos, opt => opt.MapFrom(src => src.Fotos)).ReverseMap();
            CreateMap<CriarProdutoRequest, ProdutoDto>();
            CreateMap<ProdutoDto, AtualizarProdutoRequest>().ReverseMap();

            //ESTOQUE
            CreateMap<EstoqueDto, EstoqueViewModel>().ReverseMap();
            CreateMap<EstoqueMovimentacaoDto, MovimentarEstoqueRequest>().ReverseMap();
            CreateMap<EstoqueProdutoRequest, EstoqueDto>();

            //AUTH
            CreateMap<LoginRequestDto, LoginRequest>().ReverseMap();
            CreateMap<LoginDto, LoginViewModel>();
            CreateMap<UsuarioLogadoDto, UsuarioLogadoResponse>();
            CreateMap<RefreshTokenRequestDto, RefreshTokenRequest>().ReverseMap();

            //USUARIO
            CreateMap<CriarUsuarioRequest, CriarUsuarioDto>();
            CreateMap<UsuarioLogadoDto, UsuarioViewModel>();
        }
    }
}
