using PrWebBackend.DTOs.Auth;
using PrWebBackend.DTOs.User;
using System.Collections.Generic;

namespace PrWebBackend.Services.Interfaces
{
    public interface IUserService
    {
        public List<UserDTO> GetAll();
        public LoginResponseDTO Login(LoginDTO loginDTO);
        public RegisterResponseDTO Register(RegisterDTO registerDTO);
    }
}
