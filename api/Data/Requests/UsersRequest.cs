namespace JogandoBack.API.Data.Requests
{
    public class UsersRequest
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}
