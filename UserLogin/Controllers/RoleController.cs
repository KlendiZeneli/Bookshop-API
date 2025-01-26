using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserLogin.Services;

namespace UserLogin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // Only authenticated users can access
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            var result = await _roleService.CreateRoleAsync(roleName);
            if (!result)
                return BadRequest("Role already exists or could not be created.");
            return Ok("Role created successfully.");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteRole([FromBody] string roleName)
        {
            var result = await _roleService.DeleteRoleAsync(roleName);
            if (!result)
                return BadRequest("Role not found or could not be deleted.");
            return Ok("Role deleted successfully.");
        }
    }
}
