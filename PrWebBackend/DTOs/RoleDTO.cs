using PrWebBackend.Models;
using System;

namespace PrWebBackend.DTOs
{
    public class RoleDTO
    {
        private int id;
        private string name;

        public RoleDTO() { }

        public RoleDTO(Role role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role), "Role cannot be NULL");
            Id = role.Id;
            Name = role.Name;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}
