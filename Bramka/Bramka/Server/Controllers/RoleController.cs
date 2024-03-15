using Bramka.Shared.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bramka.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController( IRoleService roleService) 
        {
            _roleService = roleService;       
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll() 
        {
            try
            {
                var roles =  await _roleService.GetAllRolesAsync();

                return Ok(roles);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getRole/{roleId}")]
        public async Task<IActionResult> GetRole(int roleId)
        {
            try
            {
                var role = await _roleService.GetRoleByIdAsync(roleId);
                return Ok(role);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
