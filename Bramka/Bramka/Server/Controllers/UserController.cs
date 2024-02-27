﻿using Bramka.Server.Interfaces;
using Bramka.Shared.DTOs.UserDTO;
using Bramka.Shared.Interfaces.Services;
using Bramka.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bramka.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogService _logService;

        public UserController(IUserService userService, ILogService logService)
        {
            _userService = userService;
            _logService = logService;
        }

        [HttpPost]
        [Route("createUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserRegistrationDTO newUser)
        {
            try
            {
                var userCreated = await _userService.CreateUserAsync(newUser);
                await _logService.CreateLogAsync(new Log { 
                                                        ActionType = "UserCreated",
                                                        Description = $"User {newUser.Name} {newUser.Surname} created.", 
                                                        UserId = userCreated, 
                                                        QrCodeId = null }); 
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
                await _logService.CreateLogAsync(new Log
                {
                    ActionType = "UserUpdated",
                    Description = $"User {userEdit.Name} {userEdit.Surname} updated.",
                    UserId = userId,
                    QrCodeId = null
                });
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
                await _logService.CreateLogAsync(new Log
                {
                    ActionType = "UserDelete",
                    Description = $"User {userId} has deleted the account.",
                    UserId = null,
                    QrCodeId = null
                });
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
