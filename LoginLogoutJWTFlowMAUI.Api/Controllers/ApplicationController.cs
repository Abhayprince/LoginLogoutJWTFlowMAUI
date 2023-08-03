using LoginLogoutJWTFlowMAUI.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginLogoutJWTFlowMAUI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetApplicationDetails()
        {
            // Get it from some config, AppSettings or Database
            var appDetails = new ApplicationDetails("Login Flow with JWT", "1.0", DateTime.Now);
            return Ok(ApiResponse<ApplicationDetails>.Success(appDetails));
        }
    }
}