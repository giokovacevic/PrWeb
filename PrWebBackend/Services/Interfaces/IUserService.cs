using PrWebBackend.DTOs;
using PrWebBackend.DTOs.Auth;
using System.Collections.Generic;

namespace PrWebBackend.Services.Interfaces
{
    public interface IUserService
    {
        public List<UserDTO> GetAll();
        public LoginResponseDTO Login(LoginDTO loginDTO);
        public void Register();
    }
}
