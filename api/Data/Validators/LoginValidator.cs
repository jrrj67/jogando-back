using api.Data.Requests;
using api.Data.Responses;
using api.Data.Services.Login;
using FluentValidation;

namespace api.Data.Validators
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        private readonly ILoginService<LoginResponse, LoginRequest> _loginService;

        public LoginValidator(ILoginService<LoginResponse, LoginRequest> loginService)
        {
            _loginService = loginService;

            RuleFor(field => field.Email)
                .NotNull();

            RuleFor(field => field.Password)
                .NotNull();

            RuleFor(field => new { field.Email, field.Password })
                .Must(fields => _loginService.VerifyEmailAndPassword(fields.Email, fields.Password)).WithMessage("Wrong credentials.");
        }
    }
}
