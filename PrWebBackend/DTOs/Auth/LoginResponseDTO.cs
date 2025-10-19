using PrWebBackend.DTOs.User;
using PrWebBackend.Models.NamespaceUser;
using System;

namespace PrWebBackend.DTOs.Auth
{
    public class LoginResponseDTO
    {
        private string token;
        private UserDTO user;

        public LoginResponseDTO(string token, Models.NamespaceUser.User user)
        {
            if (user == null) throw new ArgumentNullException();
            Token = token;
            User = new UserDTO(user);
        }

        public string Token { get => token; set => token = value; }
        public UserDTO User { get => user; set => user = value; }
    }
}
