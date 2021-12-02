namespace JogandoBack.API.Data.Models.Responses
{
    public class UsersResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public RolesResponse Role { get; set; }
    }
}
