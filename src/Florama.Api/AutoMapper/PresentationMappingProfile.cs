using AutoMapper;
using Florama.Api.ViewModel;
using Florama.Application.DTOs;

namespace Florama.Application.AutoMapper
{
    public class PresentationMappingProfile : Profile
    {
        public PresentationMappingProfile()
        {
            CreateMap<CategoriaDto, CategoriaViewModel>().ReverseMap();
        }
    }
}
