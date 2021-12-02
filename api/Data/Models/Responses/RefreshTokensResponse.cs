namespace JogandoBack.API.Data.Models.Responses
{
    public class RefreshTokensResponse
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public int UserId { get; set; }
    }
}
