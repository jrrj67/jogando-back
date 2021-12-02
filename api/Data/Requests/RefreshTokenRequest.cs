namespace JogandoBack.API.Data.Requests
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }
        public int UserId { get; set; }
    }
}
