using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManagement.Data;
using TaskManagement.Models;
using TaskManagement.Models.ViewModels;

namespace TaskManagement.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Create()
        {
            //Initializing a viewmodel to give to the view
            Project_TypeViewModel ViewModel = new Project_TypeViewModel()
            {
                Project = new Project(),
                ProjectTypes = _context.ProjectTypes.ToList()
            };

            return View("ProjectForm", ViewModel);
        }


        public IActionResult Edit(int id)
        {
            //Checking if project exists in database
            var project = _context.Projects.Find(id);

            if (project == null) { return NotFound(); }

            //Initializing a viewmodel with the project entity in it
            Project_TypeViewModel ViewModel = new Project_TypeViewModel()
            {
                Project = project,
                ProjectTypes = _context.ProjectTypes.ToList(),
            };
            return View("ProjectForm", ViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Project project)
        {

            //Checking the model validity before action
            if (!ModelState.IsValid)
            {
                //if not valid redirect to the form with posted data
                Project_TypeViewModel ViewModel = new Project_TypeViewModel()
                {
                    Project = project,
                    ProjectTypes = _context.ProjectTypes.ToList(),
                };
                return View("ProjectForm", ViewModel);
            }

            //checking if the project is new or old
            if (project.ProjectId == 0)
            {
                project.CreatorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                project.DateCreated = DateTime.Now;
                //if new add project
                _context.Projects.Add(project);
                _context.SaveChanges();

                //Creating a board or a board and a backlog according to the project type
                project.ProjectType = _context.ProjectTypes.Find(project.ProjectTypeId);
                if (project.ProjectType.ProjecTypetName == "Kanban")
                {
                    var board = new Board
                    {
                        ProjectId = project.ProjectId
                    };
                    _context.Boards.Add(board);
                }
                else if (project.ProjectType.ProjecTypetName == "Scrum")
                {
                    var board = new Board
                    {
                        ProjectId = project.ProjectId
                    };
                    _context.Boards.Add(board);

                    var backlog = new Backlog
                    {
                        ProjectId = project.ProjectId
                    };
                    _context.Backlogs.Add(backlog);
                }

                _context.SaveChanges();
            }
            else
            {
                //fetch the project from the database
                var projectExist = _context.Projects.Find(project.ProjectId);

                //if found edit the data
                if (projectExist != null)
                {
                    projectExist.Name = project.Name;
                    projectExist.Description = project.Description;

                    _context.SaveChanges();
                }
                else //report error
                {
                    return NotFound();
                }
            }

            return RedirectToAction(nameof(Index), nameof(HomeController));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var project = _context.Projects.Find(id);

            if (project == null)
            {
                return NotFound();
            }

            // Perform any additional checks before deletion if needed
            _context.Projects.Remove(project);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index), nameof(HomeController));
        }
    }
}