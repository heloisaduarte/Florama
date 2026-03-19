using Florama.Api.Request.Produto;
using FluentValidation;

public class CriarProdutoRequestValidation : AbstractValidator<CriarProdutoRequest>
{
    public CriarProdutoRequestValidation()
    {
        RuleFor(x => x.Nome)
             .NotEmpty().WithMessage("Nome do produto é obrigatório.")
             .Length(3, 100).WithMessage("Nome deve ter entre 3 e 100 caracteres.")
            .Must(nome => !string.IsNullOrWhiteSpace(nome))
            .WithMessage("Nome não pode ser vazio ou espaço.");

        RuleFor(x => x.Valor)
        .GreaterThan(0).WithMessage("Valor deve ser maior que zero.");

        RuleFor(x => x.CategoriaId)
            .NotEmpty()
            .WithMessage("CategoriaId do produto é obrigatório")
            .NotEqual(0)
            .WithMessage("CategoriaId do produto deve ser maior que zero");
    }
} 