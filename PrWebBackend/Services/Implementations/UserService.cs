using PrWebBackend.DTOs;
using PrWebBackend.DTOs.Auth;
using PrWebBackend.Models;
using PrWebBackend.Repositories.Interfaces;
using PrWebBackend.Services.Interfaces;
using System;
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

        public LoginResponseDTO Login(LoginDTO loginDTO)
        {
            User user = _userRepository.ReadByUsernameOrEmail(loginDTO.UsernameOrEmail);
            if (user == null) return null;

            Console.WriteLine("Am i here");

            bool isValid = BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password);
            if (!isValid) return null;

            // TODO: Generate JWT token

            return new LoginResponseDTO("34#3_temporary_cookie743y47ryt6", user);
        }

        public void Register() // TODO: change to string? and complete
        {
            throw new NotImplementedException();
        }
    }
}
