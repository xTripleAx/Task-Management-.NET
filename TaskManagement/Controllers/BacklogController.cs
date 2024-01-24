using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _usermanager;

        public BacklogController(ApplicationDbContext context, UserManager<IdentityUser> usermanager)
        {
            _context = context;
            _usermanager = usermanager;
        }


        [HttpGet("KanbanBacklog/{projectid}")]
        public IActionResult KanbanBacklog(int projectid)
        {
            try {
                Project project = _context.Projects.Include(p => p.ProjectType).FirstOrDefault(p => p.ProjectId == projectid);

                if (project == null)
                {
                    return View("Error404");
                }

                //Get the users that belong to this project
                var userIds = _context.UserProjects
                    .Where(x => x.ProjectId == project.ProjectId)
                    .Select(x => x.MemberId)
                    .ToList();

                var projectMembers = _context.Users
                    .Where(user => userIds.Contains(user.Id))
                .ToList();

                if (!projectMembers.Any(user => user.Id == _usermanager.GetUserId(User)))
                {
                    return View("Error404");
                }

                var board = _context.Boards
                    .Include(b => b.Lists).ThenInclude(list => list.Issues)
                    .FirstOrDefault(b => b.ProjectId == projectid);

                var backlog = _context.Backlogs
                    .Include(b => b.Issues)
                    .FirstOrDefault(b => b.ProjectId == projectid);

                var listsWithIssues = board.Lists.Select(list => new ListWithIssuesModel
                {
                    List = list,
                    Issues = list.Issues.ToList()
                }).ToList();

                var backlogissues = backlog.Issues.ToList();

                if (listsWithIssues == null)
                {
                    return View("Error404");
                }

                var viewModel = new AllInfoModel
                {
                    Project = project,
                    Board = board,
                    Backlog = backlog,
                    ListsWithIssues = listsWithIssues,
                    BacklogIssues = backlogissues,
                    ProjectMembers = projectMembers
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return View("Error404");
            }

        } 
    }
}
