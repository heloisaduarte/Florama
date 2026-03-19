using Florama.Domain.Entities;
using FluentValidation;

namespace Florama.Domain.Validators
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(x => x.Nome)
                .Must(nome => !string.IsNullOrWhiteSpace(nome))
                .WithMessage("Nome da categoria é obrigatório.")
                .Length(3, 100)
                .WithMessage("Nome deve ter entre 3 e 100 caracteres.");

            RuleFor(x => x.Descricao)
                .Must(descricao => !string.IsNullOrWhiteSpace(descricao))
                .WithMessage("Descrição da categoria é obrigatória.")
                .Length(3, 500)
                .WithMessage("Descrição deve ter entre 3 e 500 caracteres.");
        }
    }
}