using Florama.Api.Request.Categoria;
using FluentValidation;

public class AtualizarCategoriaRequestValidation : AbstractValidator<AtualizarCategoriaRequest>
{
    public AtualizarCategoriaRequestValidation()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id da categoria deve ser maior que zero.");

        RuleFor(x => x.Nome)
            .Must(nome => !string.IsNullOrWhiteSpace(nome))
            .WithMessage("Nome da categoria é obrigatório.")
            .Length(3, 100)
            .WithMessage("Nome deve ter entre 3 e 100 caracteres.");

        RuleFor(x => x.Descricao)
            .Must(descricao => !string.IsNullOrWhiteSpace(descricao))
            .WithMessage("Descrição da categoria é obrigatória.")
            .Length(3, 200)
            .WithMessage("Descrição deve ter entre 3 e 200 caracteres.");
    }
}