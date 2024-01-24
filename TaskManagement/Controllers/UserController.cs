using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.Controllers
{
    [Authorize]
    [Route("Users")]
    public class UserController : Controller
    {
        [HttpGet("AllUsers")]
        public IActionResult Index()
        {
            return View("AllUsers");
        }
    }
}
