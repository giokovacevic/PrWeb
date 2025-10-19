using Microsoft.AspNetCore.Hosting;
using PrWebBackend.DTOs.Auth;
using PrWebBackend.DTOs.User;
using PrWebBackend.Models.NamespaceUser;
using PrWebBackend.Repositories.Interfaces;
using PrWebBackend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace PrWebBackend.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment _environment;

        public UserService(IUserRepository userRepository, IWebHostEnvironment environment)
        {
            _userRepository = userRepository;
            _environment = environment;
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

        public RegisterResponseDTO Register(RegisterDTO registerDTO)
        {
            bool isValid = true;
            RegisterResponseDTO response = new RegisterResponseDTO();
            List<User> users = _userRepository.ReadAll();
            foreach( User user in users)
            {
                if(user.Username.Equals(registerDTO.Username))
                {
                    isValid = false;
                    response.Messages.Add($"Username {registerDTO.Username} already exists.");
                }

                if(user.Email.Equals(registerDTO.Email))
                {
                    isValid = false;
                    response.Messages.Add($"Email {registerDTO.Email} already exists.");
                }
            }

            if (registerDTO.Username.Trim().Length < 1)
            {
                isValid = false;
                response.Messages.Add($"Not a valid username");
            }

            if (registerDTO.Password.Length < 8)
            {
                isValid = false;
                response.Messages.Add($"Password has to be at least 8 characters long.");
            }

            string imageUrl = null;
            if (isValid)
            {
                if (registerDTO.Image != null && registerDTO.Image.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(registerDTO.Image.FileName);
                    
                    string filePath = Path.Combine(uploadsFolder, fileName);

                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        registerDTO.Image.CopyTo(stream);
                    }

                    imageUrl = "/uploads/" + fileName;
                }
            }

            // TODO: VALIDATE EMAIL?

            response.Successful = isValid;
            if(response.Successful)
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password, workFactor: 10);
                User user = new User(-1, registerDTO.Username.Trim(), registerDTO.Email, hashedPassword, imageUrl, null);
                _userRepository.CreateUser(user);
            }
            return response;
        }
    }
}
