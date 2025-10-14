using PrWebBackend.DTOs;
using PrWebBackend.Models;
using PrWebBackend.Repositories.Interfaces;
using PrWebBackend.Services.Interfaces;
using System.Collections.Generic;

namespace PrWebBackend.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserDTO> GetAll()
        {
            List<UserDTO> userDTOs = new List<UserDTO>();
            List<User> users = _userRepository.ReadAll();
            foreach(User user in users)
            {
                if (user != null) userDTOs.Add(new UserDTO(user));
            }
            return userDTOs;
        }
    }
}
