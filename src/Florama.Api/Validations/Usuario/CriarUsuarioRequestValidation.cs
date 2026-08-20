using Florama.Api.Request.Usuario;
using FluentValidation;

namespace Florama.Api.Validations.Usuario
{
    public class CriarUsuarioRequestValidation : AbstractValidator<CriarUsuarioRequest>
    {
        public CriarUsuarioRequestValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .Length(3, 100).WithMessage("Nome deve ter entre 3 e 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatório.")
                .EmailAddress().WithMessage("Email inválido.");

            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("CPF é obrigatório.");

            RuleFor(x => x.DataNascimento)
                .NotEmpty().WithMessage("Data de nascimento é obrigatória.")
                .Must(data => data < DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Data de nascimento inválida.");

            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage("Telefone é obrigatório.");

            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("Senha é obrigatória.")
                .MinimumLength(8).WithMessage("Senha deve ter no mínimo 8 caracteres.")
                .Matches(@"[A-Z]").WithMessage("A senha deve conter ao menos uma letra maiúscula.")
                .Matches(@"[a-z]").WithMessage("A senha deve conter ao menos uma letra minúscula.")
                .Matches(@"[0-9]").WithMessage("A senha deve conter ao menos um número.");

            RuleFor(x => x.ConfirmarSenha)
                .NotEmpty().WithMessage("Confirmação de senha é obrigatória.")
                .Equal(x => x.Senha).WithMessage("Senhas não conferem.");
        }
    }
}
