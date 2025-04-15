using BookingSystem.Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using BookingSystem.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _userService;

        public UserController(IUserInterface userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] SignUpDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data");

            var user = await _userService.CreateUser(dto);

            if (user == "username")
                return BadRequest("Username already exist.");
            if (user == "email")
                return BadRequest("Email already exist.");

            return Created("User successfully created!", user);
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data");

            var user = await _userService.Login(dto);

            if (user == null)
                return Unauthorized("Invalid login attempt");

            var (token, refreshToken) = await _userService.GenerateJwtToken(user);
            return Ok(new
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                UserId = user.Id,
                Message = "Login successfull."
            });
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody] TokenDto dto)
        {
            if (dto == null || String.IsNullOrEmpty(dto.RefreshToken))
                return BadRequest("Invalid data");

            var user = await _userService.GetUserByToken(dto.RefreshToken);

            if (user == null)
                return Unauthorized("Invalid user.");

            var isValid = await _userService.ValidateRefreshToken(dto.RefreshToken);

            if (!isValid)
                return Unauthorized();

            var (newAccessToken, newRefreshToken) = await _userService.GenerateJwtToken(user);
            return Ok(new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }
    }
}
