using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    [Authorize]
    [Route("Board")]
    public class BoardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BoardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("BoardDetails/{boardid}")]
        public IActionResult BoardDetails(int boardid)
        {
            var board = _context.Boards.Find(boardid);
            if (board == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("BoardDetails");
        }


        [HttpGet("KanbanBoard")]
        public IActionResult KanbanBoard(int projectid)
        {
            return View();
        }
    }
}