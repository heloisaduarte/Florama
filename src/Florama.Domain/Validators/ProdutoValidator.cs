using Florama.Domain.Entities;
using FluentValidation;

namespace Florama.Domain.Validators
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {
            RuleFor(x => x.Nome)
                .Must(nome => !string.IsNullOrWhiteSpace(nome))
                .WithMessage("Nome do produto é obrigatório.")
                .Length(3, 100)
                .WithMessage("Nome deve ter entre 3 e 100 caracteres.");

            RuleFor(x => x.Valor)
                .GreaterThan(0)
                .WithMessage("Valor deve ser maior que zero.");

            RuleFor(x => x.Categoria)
                .NotNull()
                .WithMessage("Categoria é obrigatória.");

            RuleFor(x => x.CategoriaId)
                .GreaterThan(0)
                .WithMessage("CategoriaId inválido.");
        }
    }
}