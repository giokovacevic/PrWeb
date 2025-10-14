using PrWebBackend.DTOs;
using PrWebBackend.Models;
using PrWebBackend.Repositories.Interfaces;
using PrWebBackend.Services.Interfaces;
using System.Collections.Generic;

namespace PrWebBackend.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public List<RoleDTO> GetAll()
        {
            List<RoleDTO> roleDTOs = new List<RoleDTO>();
            List<Role> roles = _roleRepository.ReadAll();
            foreach(Role role in roles)
            {
                if (role != null) roleDTOs.Add(new RoleDTO(role));
            }
            return roleDTOs;
        }
    }
}
