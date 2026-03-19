using Florama.Api.Request.Produto;
using FluentValidation;

public class ReordenarFotoItemRequestValidation : AbstractValidator<ReordenarFotoItemRequest>
{
    public ReordenarFotoItemRequestValidation()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id da foto inválido.");

        RuleFor(x => x.Ordem)
            .GreaterThan(0)
            .WithMessage("Ordem deve ser maior que zero.");
    }
}