using Florama.Domain.Abstractions;
using Florama.Domain.Notifications;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Florama.Api.Controllers
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        private readonly IHandler<DomainNotification> _notifications;
        protected ICollection<string> Erros = new List<string>();

        protected ApiController(IHandler<DomainNotification> notifications)
        {
            _notifications = notifications;
        }

        protected ActionResult CustomResponse(object? result = null)
        {
            if (!_notifications.HasNotifications())
            {
                return Ok(result);
            }

            var notificacoes = _notifications.GetNotifications();

            foreach (var notificacao in notificacoes)
            {
                Erros.Add(notificacao.Value);
            }

            return BadRequest(new ValidationProblemDetails(
                new Dictionary<string, string[]>
                {
                    { "Mensagens", Erros.ToArray() }
                }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            foreach (var error in modelState.Values.SelectMany(e => e.Errors))
            {
                Erros.Add(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Erros.Add(error.ErrorMessage);
            }

            return CustomResponse();
        }
    }
}