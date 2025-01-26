using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserLogin.Data.Models;
using UserLogin.Services;

namespace UserLogin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // Only authenticated users can access
    public class UserManagementController : ControllerBase
    {
        private readonly UserService _userService;

        public UserManagementController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] Assign_Remove_RoleModel model)
        {
            var result = await _userService.AssignRoleToUserAsync(model.UserId, model.RoleName);
            if (!result)
                return BadRequest("Role assignment failed.");
            return Ok("Role assigned successfully.");
        }

        [HttpGet("roles/{userId}")]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            var roles = await _userService.GetUserRolesAsync(userId);
            if (roles == null)
                return BadRequest("User not found.");
            return Ok(roles);
        }

        [HttpPost("remove-role")]
        public async Task<IActionResult> RemoveRoleFromUser([FromBody] Assign_Remove_RoleModel model)
        {
            var result = await _userService.RemoveRoleFromUserAsync(model.UserId, model.RoleName);
            if (!result)
                return BadRequest("Role removal failed.");
            return Ok("Role removed successfully.");
        }
    }
}
