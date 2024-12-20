using KikisCom.Server.Models.ApiModels.Kiki;
using KikisCom.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KikisCom.Server.Controllers.ver1
{
    [ApiController]
    [Route("kiki/api/v1/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Roles = "SuperAdmin")]
    public class AdminController :ControllerBase
    {
        private readonly IAdminPanelService _adminPanelService;
        public AdminController(IAdminPanelService adminPanelService)
        {
            _adminPanelService = adminPanelService;
        }

        [HttpGet("check")]
        public async Task<IActionResult> CheckMethod()
        {
            return Ok();
        }

        [HttpPost("adminRegistration")]
        public async Task<IActionResult> AdminRegistration([FromBody] RegisterInputModel registerInput)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid input");
            var result = await _adminPanelService.AdminRegistrationAsync(registerInput.Email, registerInput.UserName, registerInput.Password, registerInput.firstName, registerInput.secondName, registerInput.lastName, registerInput.IsAdmin);
            if (result.Item1.StatusCode != 200)
            {
                return result.Item1;
            }
            return Ok(result.Item2);
        }

    }
}
