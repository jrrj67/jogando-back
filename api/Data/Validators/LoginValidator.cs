using FluentValidation;
using JogandoBack.API.Data.Requests;
using JogandoBack.API.Data.Responses;
using JogandoBack.API.Data.Services.Login;

namespace JogandoBack.API.Data.Validators
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
