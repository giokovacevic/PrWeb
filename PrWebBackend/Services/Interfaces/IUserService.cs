using PrWebBackend.DTOs;
using System.Collections.Generic;

namespace PrWebBackend.Services.Interfaces
{
    public interface IUserService
    {
        public List<UserDTO> GetAll();
    }
}
