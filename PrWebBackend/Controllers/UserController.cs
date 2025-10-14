using Microsoft.AspNetCore.Mvc;
using PrWebBackend.DTOs;
using PrWebBackend.Services.Interfaces;
using System.Collections.Generic;

namespace PrWebBackend.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("all")]
        public List<UserDTO> GetAll()
        {
            return _userService.GetAll();
        }
    }
}
