using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagement.Data;
using TaskManagement.Models;
using TaskManagement.Models.ViewModels;

namespace TaskManagement.Controllers
{
    public class ListController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ListController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create(int boardid)
        {
            var board = _context.Boards.Find(boardid);
            if (board == null)
            {
                return NotFound();
            }
            else
            {
                var list = new List()
                {
                    BoardId = boardid,
                };
                return View("ListForm", list);
            }
        }


        public IActionResult Edit(int listid, int boardid)
        {
            var list = _context.Lists.FirstOrDefault(l => l.ListId == listid && l.BoardId == boardid);
            if (list == null)
            {
                return NotFound();
            }

            return View("ListForm", list);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(List list)
        {

            //Checking the model validity before action
            if (!ModelState.IsValid)
            {
                //if not valid redirect to the form with posted data
                return View("ListForm", list);
            }

            //checking if the List is new or old
            if (list.ListId == 0)
            {
                //if new add project
                _context.Lists.Add(list);
                _context.SaveChanges();

                _context.SaveChanges();
            }
            else
            {
                //fetch the project from the database
                var listExists = _context.Lists.Find(list.ListId);

                //if found edit the data
                if (listExists != null)
                {
                    listExists.Name = list.Name;
                    listExists.isListForFinish = list.isListForFinish;
                    listExists.ColumnLimit = list.ColumnLimit;

                    _context.SaveChanges();
                }
                else //report error
                {
                    return NotFound();
                }
            }

            return RedirectToAction(nameof(Index), nameof(HomeController));
        }
    }
}