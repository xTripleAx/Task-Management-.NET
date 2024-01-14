using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.Models;
using TaskManagement.Models.ViewModels;

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


        [HttpGet("KanbanBacklog/{projectid}")]
        public IActionResult KanbanBoard(int projectid)
        {

            Project project = _context.Projects.Include(p => p.ProjectType).FirstOrDefault(p => p.ProjectId == projectid);

            if (project == null)
            {
                return View("Error404");
            }

            var backlog = _context.Backlogs
                .Include(b => b.Project).FirstOrDefault(b => b.ProjectId == projectid);

            if (backlog != null)
            {
                project = backlog.Project;
            }
            else
            {
                return View("Error404"); ;
            }

            //Get the users that belong to this project
            var userIds = _context.UserProjects
                .Where(x => x.ProjectId == project.ProjectId)
                .Select(x => x.MemberId)
                .ToList();

            var projectMembers = _context.Users
                .Where(user => userIds.Contains(user.Id))
                .ToList();

            return View("Error404");

            //var listsWithIssues = board.Lists.Select(list => new ListWithIssuesModel
            //{
            //    List = list,
            //    Issues = list.Issues.ToList()
            //}).ToList();

            //if (listsWithIssues == null)
            //{
            //    return Content("Error no board data");
            //}


            //// Create the ViewModel
            //var viewModel = new AllInfoModel
            //{
            //    Project = project,
            //    Board = board,
            //    ListsWithIssues = listsWithIssues,
            //    ProjectMemebers = projectMembers
            //};

            //return View(viewModel);
        }

    }
}
