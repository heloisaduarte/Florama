using Florama.Api.Request.Produto;
using FluentValidation;

public class AtualizarProdutoRequestValidation : AbstractValidator<AtualizarProdutoRequest>
{
    public AtualizarProdutoRequestValidation()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id do produto deve ser maior que zero.");

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
    }
} 