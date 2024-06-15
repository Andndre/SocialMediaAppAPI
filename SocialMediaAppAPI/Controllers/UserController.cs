using Microsoft.AspNetCore.Mvc;
using SocialMediaAppAPI.Dto;
using SocialMediaAppAPI.Services;

namespace SocialMediaAppAPI.Controllers
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO userDto)
        {
            var response = await _userService.Register(userDto);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Data);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO userDto)
        {
            var response = await _userService.Login(userDto);
            if (!response.Success)
            {
                return Unauthorized(response.Message);
            }

            return Ok(response.Data);
        }
    }
}
