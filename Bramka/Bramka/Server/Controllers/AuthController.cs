using Bramka.Server.Interfaces;
using Bramka.Shared.DTOs.UserDTO;
using Bramka.Shared.Interfaces.Services;
using Bramka.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Bramka.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AuthController(IConfiguration configuration, IUserService userService, IRoleService roleService)
        {
            _configuration = configuration;
            _userService = userService;
            _roleService = roleService;
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

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDTO request)
        {
            User? user;
            try
            {
                user = await _userService.GetUserByEmailAsync(request.Email);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Wrong email or password");
            }

            if (user == null) 
                return BadRequest("Wrong email or password");
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return BadRequest("Wrong email or password");

            var userRole = await _roleService.GetRoleByIdAsync(user.RoleId);
            string token = CreateToken(user, userRole.Name);

            var refreshToken = GenerateRefreshToken();
            await SetRefreshTokenAsync(refreshToken, user);

            return Ok(token);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (refreshToken is null)
            {
                return Unauthorized("Refresh token is empty");
            }

            User user;
            try
            {
                user = await _userService.GetUserByRefreshTokenAsync(refreshToken)!;
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }

            if (user.TokenExpires < DateTime.Now)
                return Unauthorized("Token expired");

            var userRole = await _roleService.GetRoleByIdAsync(user.RoleId);
            string token = CreateToken(user, userRole.Name);

            var newRefreshToken = GenerateRefreshToken();
            await SetRefreshTokenAsync(newRefreshToken, user);

            //if (!user.RefreshToken.Equals(refreshToken)) return Unauthorized("Invalid Refresh Token");

            //else if (user.TokenExpires < DateTime.Now) return Unauthorized("Token expired.");

            //string token = CreateToken(user);
            //var newRefreshToken = GenerateRefreshToken();
            //SetRefreshToken(newRefreshToken);

            return Ok(token);
        }

        [HttpPost("check-admin"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> checkAdmin()
        {

            return Ok();
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Created = DateTime.Now,
                Expires = DateTime.Now.AddDays(3)
            };

            return refreshToken;
        }

        private async Task SetRefreshTokenAsync(RefreshToken refreshToken, User user)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires,
            };

            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

            Console.WriteLine(refreshToken.Token);
            await _userService.UpdateRefreshToken(refreshToken, user);
        }

        private string CreateToken(User user, string roleName)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, roleName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
