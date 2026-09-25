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

        RuleFor(x => x.Descricao)
             .NotEmpty().WithMessage("Descrição do produto é obrigatória.")
             .Length(3, 1000).WithMessage("Descrição deve ter entre 3 e 1000 caracteres.")
            .Must(descricao => !string.IsNullOrWhiteSpace(descricao))
            .WithMessage("Descrição não pode ser vazia ou espaço.");

        RuleFor(x => x.Valor)
        .GreaterThan(0).WithMessage("Valor deve ser maior que zero.");

        RuleFor(x => x.CategoriaId)
            .NotEmpty()
            .WithMessage("CategoriaId do produto é obrigatório")
            .NotEqual(0)
            .WithMessage("CategoriaId do produto deve ser maior que zero");

        RuleFor(x => x.Fotos)
            .NotNull().WithMessage("Fotos são obrigatórias.")
            .Must(fotos => fotos.Any())
            .WithMessage("O produto deve ter pelo menos uma foto.");

        RuleForEach(x => x.Fotos)
            .SetValidator(new AdicionarFotoRequestValidation());

        RuleFor(x => x.Fotos)
            .Must(fotos => fotos.Count <= 5)
            .WithMessage("O produto pode ter no máximo 5 fotos.");
    }
} 