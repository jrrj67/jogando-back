namespace JogandoBack.API.Data.Requests
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
        public int UserId { get; set; }
    }
}
