using PrWebBackend.Models;
using System;

namespace PrWebBackend.DTOs
{
    public class UserDTO
    {
        private readonly int _id;
        private readonly string _username;
        private readonly string _email;
        private readonly string _imageUrl;
        private readonly RoleDTO _role;

        public UserDTO() { }

        public UserDTO(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user), "User cannot be NULL");
            if (user.Role == null) throw new ArgumentNullException(nameof(user.Role), "Role in User cannot be NULL");

            _id = user.Id;
            _username = user.Username;
            _email = user.Email;
            _imageUrl = user.ImageUrl;
            _role = new RoleDTO(user.Role);
        }

        public int Id => _id;

        public string Username => _username;

        public string Email => _email;

        public RoleDTO Role => _role;

        public string ImageUrl => _imageUrl;
    }
}
