using FluentValidation;
using api.Data.Requests;

namespace api.Data.Validators
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
