using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Florama.Api.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var argument in context.ActionArguments)
            {
                if (argument.Value == null) continue;

                var validatorType = typeof(IValidator<>).MakeGenericType(argument.Value.GetType());
                var validator = _serviceProvider.GetService(validatorType) as IValidator;

                if (validator != null)
                {
                    var validationResult = await validator.ValidateAsync(new ValidationContext<object>(argument.Value));
                    if (!validationResult.IsValid)
                    {
                        var erros = validationResult.Errors
                            .Select(e => e.ErrorMessage)
                            .Distinct()
                            .ToArray();

                        context.Result = new BadRequestObjectResult(
                            new ValidationProblemDetails(
                                new Dictionary<string, string[]>
                                {
                                    { "Mensagens", erros }
                                })
                            {
                                Status = StatusCodes.Status400BadRequest,
                                Title = "Requisição inválida"
                            });
                        return;
                    }
                }
            }

            await next();
        }
    }
}