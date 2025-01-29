using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using UserLogin.Data.Models;
using UserLogin.Services;
using Newtonsoft.Json.Linq;

namespace UserLogin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize] //(Roles = "Admin")
    public class HtmlController : ControllerBase
    {
        // GET: api/admin/dashboard
        [HttpGet("adminPage")]
        // Ensure user is authenticated
        public IActionResult GetAdminPage()
        {
            //First, check if the user is authorized
            //if (!User.Identity.IsAuthenticated)
            //{
            //    return Unauthorized(new { message = "User is not authenticated" });
            //}

            // Path to your admin HTML file
            string htmlFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Admin", "admin.html");

            // Check if the file exists
            if (!System.IO.File.Exists(htmlFilePath))
            {
                return NotFound(new { message = "Admin page not found at " + htmlFilePath });
            }

            try
            {
                // Read the HTML content from the file
                var htmlContent = System.IO.File.ReadAllText(htmlFilePath);

                // Replace placeholders (e.g., for dynamic data like username)
                htmlContent = htmlContent.Replace("{{username}}", "Admin User");

                // Send the HTML content as the response
                return Content(htmlContent, "text/html");
            } 
            catch (Exception ex)
            {
                // Catch any internal server error and return detailed error
                return StatusCode(500, new { message = "An error occurred while reading the HTML file", error = ex.Message });
            }
        }
    }

}
