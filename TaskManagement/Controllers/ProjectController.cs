using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json.Serialization;
using TaskManagement.Data;
using TaskManagement.Models;
using TaskManagement.Models.ViewModels;
using TaskManagement.Services.Interface;

namespace TaskManagement.Controllers
{

    [Route("Project")]
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _usermanager;
        private readonly IListService _lists;

        public ProjectController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IListService lists)
        {
            _context = context;
            _usermanager = userManager;
            _lists = lists;
        }


        [HttpGet("All")]
        public IActionResult AllProjects()
        {
            return View();
        }


        [HttpGet("GetProjectsByUserId")]
        public JsonResult GetProjectsByUserId()
        {
            try
            {
                string userId = _usermanager.GetUserId(User);

                // Get the project IDs where the user is a member
                var memberProjectsIds = _context.UserProjects
                    .Where(up => up.MemberId == userId)
                    .Select(up => up.ProjectId)
                    .ToList();

                // Get projects where the user is the creator or a member, including creator names
                var projects = _context.Projects
                    .Where(p => p.CreatorId == userId || memberProjectsIds.Contains(p.ProjectId))
                    .Select(p => new
                    {
                        ProjectId = p.ProjectId,
                        ProjectName = p.Name,
                        Description = p.Description,
                        DateCreated = p.DateCreated,
                        CreatorId = p.CreatorId,
                        Creator = new
                        {
                            UserId = p.Creator.Id,
                            UserName = p.Creator.UserName,
                        },
                        ProjectTypeId = p.ProjectTypeId,
                        ProjectType = p.ProjectType
                    })
                    .ToList();

                return Json(projects);
            }
            catch (Exception ex)
            {
                // Handle exception (log it, return a specific error response, etc.)
                return Json(new { error = "An error occurred while fetching projects." });
            }
        }


        [HttpGet("Create")]
        public IActionResult Create()
        {
            //Initializing a viewmodel to give to the view
            Project_TypeViewModel ViewModel = new Project_TypeViewModel()
            {
                Project = new Models.Project(),
                ProjectTypes = _context.ProjectTypes.ToList()
            };

            return View("ProjectForm", ViewModel);
        }


        [HttpGet("Edit/{id}")]
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


        [HttpPost("Save")]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Models.Project project)
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
                //Filling the projects required data if it is new
                project.CreatorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                project.DateCreated = DateTime.Now;

                _context.Projects.Add(project);

                var board = new Board
                {
                    Project = project
                };
                _context.Boards.Add(board);

                var backlog = new Backlog
                {
                    Project = project
                };
                _context.Backlogs.Add(backlog);

                _context.SaveChanges();

                _lists.CreateDefaultLists(board.BoardId);

                _context.SaveChanges();

                var member = new UserProject
                {
                    MemberId = project.CreatorId,
                    ProjectId = project.ProjectId
                };

                _context.UserProjects.Add(member);

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

            return RedirectToAction("All");
        }


        [HttpDelete("DeleteProjectById/{id}")]
        //[ValidateAntiForgeryToken]
        public IActionResult DeleteProjectById(int id)
        {
            var project = _context.Projects.Find(id);

            if (project == null)
            {
                return NotFound();
            }

            // Perform any additional checks before deletion if needed
            _context.Projects.Remove(project);
            _context.SaveChanges();

            return NoContent();
        }
    }
}