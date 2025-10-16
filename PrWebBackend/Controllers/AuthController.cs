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
            LoginResponseDTO response = _userService.Login(loginDTO);
            if(response == null)
            {
                return Unauthorized();
            }
            return Ok(response);
        }

        [HttpPost("register")]
        public IActionResult Register([FromForm] RegisterDTO registerDTO)
        {
            RegisterResponseDTO response = _userService.Register(registerDTO);
            if (!response.Successful) return BadRequest(response);
            return StatusCode(201, response);
        }
    }
}
