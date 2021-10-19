using FluentValidation;
using PontoPlus.Manager.Domain.Entities;

namespace PontoPlus.Manager.Domain.Validators
{
    public class UsuarioMensagemValidator : AbstractValidator<UsuarioMensagem>
    {
        public UsuarioMensagemValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("A entidade não pode ser nula.")

                .NotEmpty()
                .WithMessage("A entidade não pode ser vazia.");

            RuleFor(x => x.Message)
                .NotNull()
                .WithMessage("A mensagem não pode ser nula.")

                .NotEmpty()
                .WithMessage("A mensagem não pode ser vazia.")
                
                .MaximumLength(80)
                .WithMessage("A mensagem pode ter no máximo 80 caracteres");
        }
    }
}
