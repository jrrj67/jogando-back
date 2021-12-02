namespace JogandoBack.API.Data.Models.Requests
{
    public class RefreshTokensRequest
    {
        public string Token { get; set; }
        public int UserId { get; set; }
    }
}
