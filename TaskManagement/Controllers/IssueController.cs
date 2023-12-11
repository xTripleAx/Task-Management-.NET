using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.Controllers
{
    public class IssueController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
