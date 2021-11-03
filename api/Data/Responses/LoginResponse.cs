namespace api.Data.Responses
{
    public class LoginResponse
    {
        public UsersResponse UserResponse { get; set; }
        public string Token { get; set; }
    }
}
