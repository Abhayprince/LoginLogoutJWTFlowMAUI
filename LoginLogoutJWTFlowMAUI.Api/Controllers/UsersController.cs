using LoginLogoutJWTFlowMAUI.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginLogoutJWTFlowMAUI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            // Get users from Database
            var users = new UserDto[]
            {
                new (Guid.NewGuid(), "Abhay", true),
                new (Guid.NewGuid(), "Prince", true),
                new (Guid.NewGuid(), "Alex", false),
                new (Guid.NewGuid(), "Samantha", true),
                new (Guid.NewGuid(), "Krish", false),
                new (Guid.NewGuid(), "Akshar", false)
            };
            return Ok(ApiResponse<UserDto[]>.Success(users));
        }
    }
}