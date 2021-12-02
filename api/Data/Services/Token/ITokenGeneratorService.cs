using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace JogandoBack.API.Data.Services.Token
{
    public interface ITokenGeneratorService
    {
        string GenerateToken(string issuer, string audience, DateTime notBefore, DateTime expiration, SigningCredentials credentials,
            List<Claim> claims = null);

        byte[] GetSecretKey(IConfiguration configuration, string key);

        TokenConfiguration GetTokenConfiguration(IConfiguration configuration, string key);
    }
}