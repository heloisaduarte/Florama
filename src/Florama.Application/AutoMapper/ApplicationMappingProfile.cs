using AutoMapper;
using Florama.Application.DTOs;
using Florama.Domain.Entities;

namespace Florama.Application.AutoMapper
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<CategoriaDto, Categoria>().ReverseMap();
        }
    }
}
