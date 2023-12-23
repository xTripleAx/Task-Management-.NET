using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.Controllers
{
    [Authorize]
    public class PagesController : Controller
    {
        public IActionResult UserHomePage()
        {
            return View();
        }
    }
}
