using Florama.Api.Request.Produto;
using FluentValidation;

public class AdicionarFotoRequestValidation : AbstractValidator<AdicionarFotoRequest>
{
    public AdicionarFotoRequestValidation()
    {
        RuleFor(x => x.Url)
            .Must(url => !string.IsNullOrWhiteSpace(url))
            .WithMessage("Url da foto é obrigatória.")
            .MaximumLength(1000)
            .WithMessage("Url muito longa.");
    }
}