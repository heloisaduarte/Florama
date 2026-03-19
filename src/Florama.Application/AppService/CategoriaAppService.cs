using AutoMapper;
using Florama.Application.AppService;
using Florama.Application.DTOs;
using Florama.Application.Interfaces;
using Florama.Domain.Abstractions;
using Florama.Domain.Entities;
using Florama.Domain.Interfaces.Repositories;
using Florama.Domain.Notifications;
using FluentValidation;

namespace Florama.Application.AppServices
{
    public class CategoriaAppService : BaseAppService, ICategoriaAppService
    {
        private readonly ICategoriaRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<Categoria> _categoriaValidator;
        public CategoriaAppService(
            ICategoriaRepository repository,
            IUnitOfWork uow,
            IHandler<DomainNotification> notifications,
            IMapper mapper,
            IValidator<Categoria> categoriaValidator) : base(uow, notifications)
        {
            _repository = repository;
            _mapper = mapper;
            _categoriaValidator = categoriaValidator;
        }

        public async Task<CategoriaDto> Criar(CategoriaDto dto)
        {
            var categoria = _mapper.Map<Categoria>(dto);

            if (!Validar(_categoriaValidator, categoria))
                return null;

            await _repository.Add(categoria);

            await CommitAsync();

            return _mapper.Map<CategoriaDto>(categoria);
        }

        public async Task<CategoriaDto> Atualizar(CategoriaDto dto)
        {
            var categoria = await _repository.GetByIdAsync(dto.Id);

            if (categoria is null)
            {
                NotificarErro("Categoria não encontrada.");
                return null;
            }

            categoria.DefinirNome(dto.Nome);

            categoria.DefinirDescricao(dto.Descricao);

            if (!Validar(_categoriaValidator, categoria))
                return null;

             await _repository.Update(categoria);

            await CommitAsync();

            return _mapper.Map<CategoriaDto>(categoria);
        }

        public async Task<IEnumerable<CategoriaDto>> ObterTodos()
        {
            var categoria = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<CategoriaDto>>(categoria);
        }

        public async Task<CategoriaDto> ObterPorId(long id)
        {
            var categoria = await _repository.GetByIdAsync(id);

            return _mapper.Map<CategoriaDto>(categoria);
        }
    }
}