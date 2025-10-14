using PrWebBackend.DTOs;
using System.Collections.Generic;

namespace PrWebBackend.Services.Interfaces
{
    public interface IRoleService
    {
        public List<RoleDTO> GetAll();
    }
}
