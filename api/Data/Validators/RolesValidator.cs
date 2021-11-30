using JogandoBack.API.Data.Requests;
using FluentValidation;

namespace JogandoBack.API.Data.Validators
{
    public class RolesValidator : AbstractValidator<RolesRequest>
    {
        public RolesValidator()
        {
            RuleFor(field => field.Name)
                .MinimumLength(2)
                .MaximumLength(200)
                .NotNull();
        }
    }
}
