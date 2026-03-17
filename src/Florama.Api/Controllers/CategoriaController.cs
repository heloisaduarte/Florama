using AutoMapper;
using Florama.Api.ViewModel;
using Florama.Application.DTOs;
using Florama.Application.Interfaces;
using Florama.Domain.Abstractions;
using Florama.Domain.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace Florama.Api.Controllers
{
    [Route("api/[controller]")]
    public class CategoriaController : ApiController
    {
        private readonly ICategoriaAppService _appService;
        private readonly IMapper _mapper;

        public CategoriaController(
            ICategoriaAppService appService,
            IHandler<DomainNotification> notifications,
            IMapper mapper
        ) : base(notifications)
        {
            _appService = appService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CategoriaViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var dto = _mapper.Map<CategoriaDto>(viewModel);

            var result = await _appService.Criar(dto);

            if (result == null)
                return CustomResponse();

            return CustomResponse(_mapper.Map<CategoriaViewModel>(result));
        }
    }
}