using AutoMapper;
using Florama.Api.Request.Categoria;
using Florama.Api.Request.Produto;
using Florama.Api.ViewModel;
using Florama.Application.DTOs;

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
            CreateMap<ProdutoDto, ProdutoViewModel>().ReverseMap();
            CreateMap<ProdutoDto, CriarProdutoRequest>().ReverseMap();
            CreateMap<ProdutoDto, AtualizarProdutoRequest>().ReverseMap();
        }
    }
}
