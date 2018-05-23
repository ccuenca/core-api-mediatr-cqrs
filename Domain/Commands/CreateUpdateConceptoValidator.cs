using FluentValidation;
using TestMediaTR.Domain.Commands;

namespace TestMediaTR.Domain.Commands
{
    public class CreateUpdateConceptoCommandValidator : AbstractValidator<CreateUpdateConceptosCommand>
    {
        public CreateUpdateConceptoCommandValidator()
        {
            RuleFor(c => c.Concepto.Codigo)
                    .Length(2)
                    .WithMessage("El codigo debe tener 2 caracteres.");
        }
    }
}