using Florama.Domain.DomainObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Florama.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Erro não tratado.");

            if (context.Exception is DomainException domainException)
            {
                context.Result = new BadRequestObjectResult(
                    new ValidationProblemDetails(
                        new Dictionary<string, string[]>
                        {
                            { "Mensagens", new[] { domainException.Message } }
                        })
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Requisição inválida"
                    });

                context.ExceptionHandled = true;
                return;
            }

            if (context.Exception is DbUpdateConcurrencyException)
            {
                context.Result = new ObjectResult(
                    new ProblemDetails
                    {
                        Status = StatusCodes.Status409Conflict,
                        Title = "Conflito",
                        Detail = "Os dados foram alterados por outro usuário. Recarregue e tente novamente."
                    })
                {
                    StatusCode = StatusCodes.Status409Conflict
                };

                context.ExceptionHandled = true;
                return;
            }

            context.Result = new ObjectResult(
                new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Erro interno no servidor.",
                    Detail = "Ocorreu um erro inesperado. Tente novamente mais tarde."
                })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}