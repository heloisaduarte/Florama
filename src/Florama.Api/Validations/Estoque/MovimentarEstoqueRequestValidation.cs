using Florama.Api.Request.Categoria;
using Florama.Api.Request.Estoque;
using FluentValidation;

namespace Florama.Api.Validations.Estoque
{
    public class MovimentarEstoqueRequestValidation : AbstractValidator<MovimentarEstoqueRequest>
    {
        public MovimentarEstoqueRequestValidation()
        {
            RuleFor(x => x.ProdutoId)
           .NotEmpty()
           .WithMessage("ProdutoId do estoque é obrigatório")
           .NotEqual(0)
           .WithMessage("ProdutoId do estoque deve ser maior que zero");
        }
    }
}
