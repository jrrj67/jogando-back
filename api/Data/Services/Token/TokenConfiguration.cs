using System;

namespace JogandoBack.API.Data.Services.Token
{
    public class TokenConfiguration
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public DateTime NotBefore { get; set; }
        public DateTime Expiration { get; set; }
    }
}