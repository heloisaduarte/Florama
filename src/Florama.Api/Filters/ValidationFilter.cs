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

                // Procura um validator registrado para o tipo do argumento
                var validatorType = typeof(IValidator<>).MakeGenericType(argument.Value.GetType());
                var validator = _serviceProvider.GetService(validatorType) as IValidator;

                if (validator != null)
                {
                    var validationResult = await validator.ValidateAsync(new ValidationContext<object>(argument.Value));
                    if (!validationResult.IsValid)
                    {
                        context.Result = new BadRequestObjectResult(
                            validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
                        );
                        return; // para a execução do controller
                    }
                }
            }

            await next();
        }
    }
}
