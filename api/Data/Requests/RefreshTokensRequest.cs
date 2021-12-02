namespace JogandoBack.API.Data.Requests
{
    public class RefreshTokensRequest
    {
        public string Token { get; set; }
        public int UserId { get; set; }
    }
}
