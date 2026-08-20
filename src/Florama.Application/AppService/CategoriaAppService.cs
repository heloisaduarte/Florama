using AutoMapper;
using Florama.Application.AppService;
using Florama.Application.DTOs;
using Florama.Application.Interfaces;
using Florama.Domain.Abstractions;
using Florama.Domain.DomainObjects;
using Florama.Domain.Entities;
using Florama.Domain.Interfaces.Repositories;
using Florama.Domain.Notifications;

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
            IMapper mapper) : base(uow, notifications)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CategoriaDto?> Criar(CategoriaDto dto)
        {
            try
            {
                var categoria = new Categoria(dto.Nome, dto.Descricao);

                await _repository.Adicionar(categoria);

                await CommitAsync();

                return _mapper.Map<CategoriaDto>(categoria);
            }
            catch (DomainException ex)
            {
                NotificarErro(ex.Message);
                return null;
            }
        }

        public async Task<CategoriaDto?> Atualizar(CategoriaDto dto)
        {
            var categoria = await _repository.ObterPorIdAsync(dto.Id);

            if (categoria is null)
            {
                NotificarErro("Categoria não encontrada.");
                return null;
            }

            try
            {
                categoria.DefinirNome(dto.Nome);
                categoria.DefinirDescricao(dto.Descricao);

                await _repository.Atualizar(categoria);

                await CommitAsync();

                return _mapper.Map<CategoriaDto>(categoria);
            }
            catch (DomainException ex)
            {
                NotificarErro(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<CategoriaDto>> ObterTodos()
        {
            var categorias = await _repository.ObterTodosAsync();

            return _mapper.Map<IEnumerable<CategoriaDto>>(categorias);
        }

        public async Task<CategoriaDto?> ObterPorId(long id)
        {
            var categoria = await _repository.ObterPorIdAsync(id);

            return _mapper.Map<CategoriaDto>(categoria);
        }
    }
}