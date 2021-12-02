namespace JogandoBack.API.Data.Models
{
    public class RefreshToken : BaseModel
    {
        public string Token { get; set; }
        public int UserId { get; set; }
    }
}
