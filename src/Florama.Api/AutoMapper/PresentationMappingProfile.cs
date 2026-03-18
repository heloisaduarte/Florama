using AutoMapper;
using Florama.Api.Request;
using Florama.Api.ViewModel;
using Florama.Application.DTOs;

namespace Florama.Api.AutoMapper
{
    public class PresentationMappingProfile : Profile
    {
        public PresentationMappingProfile()
        {
            CreateMap<CategoriaDto, CategoriaViewModel>().ReverseMap();
            CreateMap<CategoriaDto, CriarCategoriaRequest>().ReverseMap();
        }
    }
}
