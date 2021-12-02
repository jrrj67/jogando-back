using FluentValidation;
using JogandoBack.API.Data.Requests;
using JogandoBack.API.Data.Services.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace JogandoBack.API.Data.Validators
{
    public class RefreshTokensValidator : AbstractValidator<RefreshTokensRequest>
    {
        private readonly ITokenGeneratorService _tokenGeneratorService;
        public IConfiguration Configuration { get; set; }

        public RefreshTokensValidator(ITokenGeneratorService tokenGeneratorService)
        {
            _tokenGeneratorService = tokenGeneratorService;

            RuleFor(field => field.Token)
                .Must(rt => IsValid(rt))
                    .WithMessage("Token invalid.")
                .NotNull();
        }

        public bool IsValid(string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenConfiguration = _tokenGeneratorService.GetTokenConfiguration(Configuration, "Refresh");

            var validationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(_tokenGeneratorService.GetSecretKey(Configuration, "Token")),
                ValidIssuer = tokenConfiguration.Issuer,
                ValidAudience = tokenConfiguration.Audience,
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                tokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken validatedToken);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
