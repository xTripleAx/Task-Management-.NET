using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.Models;
using TaskManagement.Models.ViewModels;

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

        [HttpGet("KanbanBoard")]
        public IActionResult KanbanBoard(int projectid)
        {

            if (projectid == 0)
            {
                return Content("Error No Project");
            }

            Project project = new Project();

            var board = _context.Boards
                .Include(b => b.Project)
                .Include(b => b.Lists).ThenInclude(list => list.Issues)
                .FirstOrDefault(b => b.ProjectId == projectid);

            if (board != null)
            {
                project = board.Project;
            }
            else
            {
                return Content("Error No Board"); ;
            }

            //Get the users that belong to this project
            var userIds = _context.UserProjects
                .Where(x => x.ProjectId == project.ProjectId)
                .Select(x => x.MemberId)
                .ToList();

            var projectMembers = _context.Users
                .Where(user => userIds.Contains(user.Id))
                .ToList();


            var listsWithIssues = board.Lists.Select(list => new ListWithIssuesModel
            {
                List = list,
                Issues = list.Issues.ToList()
            }).ToList();

            if (listsWithIssues == null)
            {
                return Content("Error no board data");
            }

            Console.WriteLine(listsWithIssues.Count());

            // Create the ViewModel
            var viewModel = new AllInfoModel
            {
                Project = project,
                Board = board,
                ListsWithIssues = listsWithIssues,
                ProjectMemebers = projectMembers
            };

            return View(viewModel);
        }
    }
}