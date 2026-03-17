using AutoMapper;
using Florama.Application.AppService;
using Florama.Application.DTOs;
using Florama.Application.Interfaces;
using Florama.Domain.Abstractions;
using Florama.Domain.Entities;
using Florama.Domain.Interfaces.Repositories;
using Florama.Domain.Notifications;
using Florama.Domain.Validators;

namespace Florama.Application.AppServices
{
    public class CategoriaAppService : BaseAppService, ICategoriaAppService
    {
        private readonly ICategoriaRepository _repository;
        private readonly IMapper _mapper;

        public CategoriaAppService(
            ICategoriaRepository repository,
            IUnitOfWork uow,
            IHandler<DomainNotification> notifications,
            IMapper mapper
        ) : base(uow, notifications)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CategoriaDto> Criar(CategoriaDto dto)
        {
            var categoria = _mapper.Map<Categoria>(dto);

            if (!Validar(new CategoriaValidator(), categoria))
                return null;

            await _repository.Add(categoria);

            await CommitAsync();

            return _mapper.Map<CategoriaDto>(categoria);
        }
    }
}