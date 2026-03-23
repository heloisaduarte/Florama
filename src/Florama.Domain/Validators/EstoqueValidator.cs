using Florama.Domain.Entities;
using FluentValidation;

namespace Florama.Domain.Validators
{
    public class EstoqueValidator : AbstractValidator<Estoque>
    {
        public EstoqueValidator()
        {

            RuleFor(x => x.Produto)
                .NotNull()
                .WithMessage("Produto é obrigatóriao.");

            RuleFor(x => x.ProdutoId)
                .GreaterThan(0)
                .WithMessage("ProdutoId inválido.");
        }
    }
}