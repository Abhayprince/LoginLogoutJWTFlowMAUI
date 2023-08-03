using LoginLogoutJWTFlowMAUI.Api.Services;
using LoginLogoutJWTFlowMAUI.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginLogoutJWTFlowMAUI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto, CancellationToken cancellationToken = default)
        {
            var response = await _authService.LoginAsync(dto, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}