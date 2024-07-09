using Backend.Dto;
using Backend.Service;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController:ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] UserLoginDto userLoginDto)
        {
            var user = await _userService.FindByUsername(userLoginDto.Username);

            if(user == null)
            {
                return BadRequest("User not found");
            }

            if(!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.PasswordHash))
            {
                return BadRequest("Wrong Password");
            }

            string token = _userService.CreateToken(userLoginDto.Username, userLoginDto.Password);

            if(token == null)
            {
                return Unauthorized(new {message="Username or password incorrect"});
            }

            return Ok(token);
        }
    }
}
