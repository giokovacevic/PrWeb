using Microsoft.AspNetCore.Http;

namespace PrWebBackend.DTOs.Auth
{
    public class RegisterDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile Image { get; set; }
    }
}
