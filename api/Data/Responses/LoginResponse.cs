﻿namespace api.Data.Responses
{
    public class LoginResponse
    {
        public UsersResponse User { get; set; }
        public string Token { get; set; }
    }
}
