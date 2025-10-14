using Microsoft.AspNetCore.Mvc;
using PrWebBackend.DTOs.Auth;
using PrWebBackend.Services.Interfaces;
using System;

namespace PrWebBackend.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            Console.WriteLine(loginDTO.UsernameOrEmail + " " + loginDTO.Password);
            LoginResponseDTO response = _userService.Login(loginDTO);
            if(response == null)
            {
                return Unauthorized();
            }
            return Ok(response);
        }

        [HttpPost("register")]
        public int Register() // TODO: [FromBody] RegisterDTO. ...
        {
            return -1;
        }
    }
}
