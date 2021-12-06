using FluentValidation;
using JogandoBack.API.Data.Models.Requests;
using JogandoBack.API.Data.Services.Token;

namespace JogandoBack.API.Data.Validators
{
    public class RefreshTokensValidator : AbstractValidator<RefreshTokensBaseRequest>
    {
        private readonly IRefreshTokenService _refreshTokenService;

        public RefreshTokensValidator(IRefreshTokenService refreshTokenService)
        {
            _refreshTokenService = refreshTokenService;

            RuleFor(field => field.Token)
                .Must(rt => _refreshTokenService.IsValid(rt))
                    .WithMessage("Token invalid.")
                .NotNull();
        }
    }
}
