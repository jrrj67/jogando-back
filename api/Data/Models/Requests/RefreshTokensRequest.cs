namespace JogandoBack.API.Data.Models.Requests
{
    public class RefreshTokensBaseRequest
    {
        public string Token { get; set; }
    }

    public class RefreshTokensRequest : RefreshTokensBaseRequest
    {
        public int UserId { get; set; }
    }
}
