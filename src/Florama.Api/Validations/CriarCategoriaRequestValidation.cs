using Florama.Api.Request;
using FluentValidation;

public class CriarCategoriaRequestValidation : AbstractValidator<CriarCategoriaRequest>
{
    public CriarCategoriaRequestValidation()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("Nome da categoria é obrigatório");

        RuleFor(x => x.Descricao)
            .NotEmpty()
            .WithMessage("Descrição da categoria é obrigatório");
    }
}