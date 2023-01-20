using Microsoft.AspNetCore.Mvc;
using WebApi.Core.Entities.Dtos;
using WebApi.Service.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:Controller
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromForm] UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

          var result = _authService.CreateAccessToken(userToLogin.Data);
          if (result.Success)
          {
              return Ok(result.Data);
          }

          return BadRequest(result.Message);
        }

        [HttpPost("Register")]
        public IActionResult Register([FromForm] UserForRegisterDto userForRegisterDto)
        {

            var userExists = _authService.UserExits(userForRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }


    }
}
