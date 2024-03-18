using Bramka.Server.Interfaces;
using Bramka.Shared.DTOs.UserDTO;
using Bramka.Shared.Interfaces.Services;
using Bramka.Shared.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using System.Security.Claims;

namespace Bramka.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("createUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserRegistrationDTO newUser)
        {
            try
            {
                var userCreated = await _userService.CreateUserAsync(newUser);
                return Ok(userCreated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //[Authorize]
        [HttpPut]
        [Route("edit/{userId}")]
        public async Task<IActionResult> EditUserDateAsync([FromBody] UserEditDTO userEdit, Guid userId)
        {
            try
            {
                await _userService.UpdateUserAsync(userId, userEdit);
                return Ok();

            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            
        }

        //[Authorize]
        [HttpGet]
        [Route("info/{userId}")]
        public async Task<IActionResult> UserPersonalIngoAsync(Guid userId)
        {
            try
            {
                var userInfo = await _userService.GetUserByIdAsync(userId);
                return Ok(userInfo);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //[Authorize]
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteUserAccount(Guid userId)
        {

            try
            {
                var success = await _userService.DeleteUserAsync(userId);
                if (!success)
                {
                    return NotFound();
                }
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var name = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            await Console.Out.WriteLineAsync(email);
            await Console.Out.WriteLineAsync(name);

            await _userService.SendResetPasswordEmailAsync(email, name);

            return Ok();
        }

        [HttpPost]
        [Route("reset-password/confirm")]
        public async Task<IActionResult> ResetPasswordConfirm(string? code, ResetPasswordDTO resetPasswordDTO)
        {
            if (code == null)
                return BadRequest("Code is empty");

            if (resetPasswordDTO.Password != resetPasswordDTO.RepeatPassword)
                return BadRequest("Passwords do not match");

            var response = await _userService.ResetPasswordAsync(code, resetPasswordDTO.Password);

            if (response > 1)
                return Ok("Password is successfully changed");

            return BadRequest("Reset password was not successful");
        }
    }
}
