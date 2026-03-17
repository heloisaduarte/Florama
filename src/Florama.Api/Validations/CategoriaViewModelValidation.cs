using Florama.Api.ViewModel;
using FluentValidation;

public class CategoriaViewModelValidation : AbstractValidator<CategoriaViewModel>
{
    public CategoriaViewModelValidation()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("Nome da categoria é obrigatório");

        RuleFor(x => x.Descricao)
            .NotEmpty()
            .WithMessage("Descrição da categoria é obrigatório");
    }
}