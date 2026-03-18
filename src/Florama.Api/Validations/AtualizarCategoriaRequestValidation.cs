using Florama.Api.Request;
using FluentValidation;

public class AtualizarCategoriaRequestValidation: AbstractValidator<AtualizarCategoriaRequest>
{
    public AtualizarCategoriaRequestValidation()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id da categoria é obrigatório")
            .NotEqual(0)
            .WithMessage("Id da categoria deve ser maior que zero.");

        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("Nome da categoria é obrigatório");

        RuleFor(x => x.Descricao)
            .NotEmpty()
            .WithMessage("Descrição da categoria é obrigatório");
    }
}