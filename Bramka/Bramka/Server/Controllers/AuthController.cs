using Bramka.Server.Interfaces;
using Bramka.Shared.DTOs.UserDTO;
using Bramka.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bramka.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(UserRegistrationDTO request)
        {
            if(_userService.IsEmailExist(request.Email))
            {
                return BadRequest("Account with this email is already created");
            }

            await _userService.CreateUserAsync(request);

            return Ok("User is successfully created");
        }
    }
}
