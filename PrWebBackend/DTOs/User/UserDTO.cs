using PrWebBackend.Models;
using PrWebBackend.Models.NamespaceUser;
using System;

namespace PrWebBackend.DTOs.User
{
    public class UserDTO
    {
        private int id;
        private string username;
        private string email;
        private string imageUrl;
        private RoleDTO role;

        public UserDTO() { }

        public UserDTO(Models.NamespaceUser.User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user), "User cannot be NULL");
            if (user.Role == null) throw new ArgumentNullException(nameof(user.Role), "Role in User cannot be NULL");

            Id = user.Id;
            Username = user.Username;
            Email = user.Email;
            ImageUrl = user.ImageUrl;
            Role = new RoleDTO(user.Role);
        }

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public string Email { get => email; set => email = value; }
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }
        public RoleDTO Role { get => role; set => role = value; }
    }
}
