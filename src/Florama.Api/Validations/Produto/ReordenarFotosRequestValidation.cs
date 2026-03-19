using Florama.Api.Request.Produto;
using FluentValidation;

public class ReordenarFotosRequestValidation : AbstractValidator<ReordenarFotosRequest>
{
    public ReordenarFotosRequestValidation()
    {
        RuleFor(x => x.Fotos)
            .NotNull().WithMessage("Fotos são obrigatórias.")
            .Must(fotos => fotos.Any())
            .WithMessage("Deve informar ao menos uma foto.");

        RuleForEach(x => x.Fotos)
            .SetValidator(new ReordenarFotoItemRequestValidation());

        RuleFor(x => x.Fotos)
            .Must(fotos => fotos
                .Select(f => f.Ordem)
                .Distinct()
                .Count() == fotos.Count)
            .WithMessage("Não pode haver ordens duplicadas.");
    }
}