using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TheTruth.Api.Data.Models
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
