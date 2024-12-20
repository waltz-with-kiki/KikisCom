using KikisCom.Server.Models.ApiModels.Kiki;
using KikisCom.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KikisCom.Server.Controllers.ver1
{
    [ApiController]
    [Route("/kiki/api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IRegistrationService _registrationService;
        public AuthController(IAuthService authService, IRegistrationService registrationService)
        {
            _authService = authService;
            _registrationService = registrationService;
        }
        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] UserRegisterInputModel registerInput)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid input");
            var result = await _registrationService.UserRegistrationAsync(registerInput.Email, registerInput.UserName, registerInput.Password);
            if (result.Item1.StatusCode != 200)
            {
                return result.Item1;
            }
            return Ok(result.Item2);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthInputModel authInput)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid input");

            var result = await _authService.Login(authInput.UserName, authInput.Password, authInput.RememberMe);
            return result;
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _authService.Logout();
            return result;
        }

    }
}
