using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserLogin.Data.Models;
using UserLogin.Services;

namespace UserLogin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   // [Authorize(Policy = "Jwt_Or_Identity")]
    //[Authorize(Roles = "Admin")] // Only authenticated users can access
    public class UserManagementController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AuthService _authService;

        public UserManagementController(UserService userService, AuthService authService)
        {
            _userService = userService;
            _authService = authService;
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

        [HttpPost("update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserModel model)
        {
            if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Email))
            {
                return BadRequest("Username and Email cannot be empty.");
            }

            var result = await _userService.UpdateUserAsync(model.UserId, model.UserName, model.Email);
            if (!result)
            {
                return BadRequest("Failed to update user details.");
            }

            // Send confirmation email via AuthService
            var emailSent = await _authService.GenerateAndSendConfirmationEmailAsync(model.UserId);
            if (!emailSent)
            {
                return BadRequest("User updated, but failed to send confirmation email.");
            }

            return Ok("User updated successfully. A confirmation email has been sent.");
        }
    }
}
