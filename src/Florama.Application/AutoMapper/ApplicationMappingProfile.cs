using AutoMapper;
using Florama.Application.DTOs;
using Florama.Application.DTOs.Auth;
using Florama.Domain.Entities;

namespace Florama.Application.AutoMapper
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<CategoriaDto, Categoria>().ReverseMap();
            CreateMap<ProdutoDto, Produto>()
                .ForMember(dest => dest.Fotos, opt => opt.MapFrom(src => src.Fotos))
                .ForMember(dest => dest.Estoque, opt => opt.MapFrom(src => src.Estoque))
                .ReverseMap();
            CreateMap<ProdutoFotoDto, ProdutoFoto>().ReverseMap();
            CreateMap<EstoqueDto, Estoque>().ReverseMap();
            CreateMap<EstoqueMovimentacaoDto, EstoqueMovimentacao>().ReverseMap();

            CreateMap<Usuario, UsuarioLogadoDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Endereco))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));
        }
    }
}
