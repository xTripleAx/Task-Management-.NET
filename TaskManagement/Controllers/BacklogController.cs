using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Data;

namespace TaskManagement.Controllers
{
    [Authorize]
    [Route("Backlog")]
    public class BacklogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BacklogController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("BacklogDetails/{backlogid}")]
        public IActionResult BacklogDetails(int backlogid)
        {
            var backlog = _context.Backlogs.Find(backlogid);
            if (backlog == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("BacklogDetails");
        }
    }
}
