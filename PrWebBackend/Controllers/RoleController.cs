using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrWebBackend.DTOs.User;
using PrWebBackend.Services.Interfaces;
using System.Collections.Generic;

namespace PrWebBackend.Controllers
{
    [ApiController]
    [Route("roles")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("all")]
        [Authorize(Roles = "admin")]
        public List<RoleDTO> GetAll()
        {
            return _roleService.GetAll();
        }
    }
}
