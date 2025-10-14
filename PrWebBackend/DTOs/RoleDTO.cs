using PrWebBackend.Models;
using System;

namespace PrWebBackend.DTOs
{
    public class RoleDTO
    {
        private readonly int _id;
        private readonly string _name;

        public RoleDTO() { }

        public RoleDTO(Role role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role), "Role cannot be NULL");
            _id = role.Id;
            _name = role.Name;
        }

        public int Id => _id;

        public string Name => _name;
    }
}
