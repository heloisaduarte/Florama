using Florama.Domain.Entities;
using FluentValidation;

namespace Florama.Domain.Validators
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(x => x.Nome)
         .NotEmpty()
         .WithMessage("Nome da categoria é obrigatório")
         .MaximumLength(100)
         .WithMessage("Nome da categoria deve ter no máximo 50 caracteres")
         .MinimumLength(3)
         .WithMessage("Nome deve ter no mínimo 3 caracteres");

            RuleFor(x => x.Descricao)
         .MaximumLength(500)
         .WithMessage("Descrição deve ter no máximo 500 caracteres")
         .MinimumLength(10)
         .WithMessage("Nome deve ter no mínimo 3 caracteres");
        }
    }
}
