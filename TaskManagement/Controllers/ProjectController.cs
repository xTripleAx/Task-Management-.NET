using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json.Serialization;
using TaskManagement.Data;
using TaskManagement.Models;
using TaskManagement.Models.ViewModels;
using TaskManagement.Services.Interface;

namespace TaskManagement.Controllers
{
    [Authorize]
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


        [HttpPost("Save")]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Project project)
        {
            try
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

                    return RedirectToAction("All");
                }
                else
                {
                    //fetch the project from the database
                    var projectExist = _context.Projects.Include(p => p.ProjectType).FirstOrDefault(p => p.ProjectId == project.ProjectId);

                    //if found edit the data
                    if (projectExist != null)
                    {
                        BaseViewModel m = new BaseViewModel();
                        m.Project = projectExist;
                        if (projectExist.CreatorId == _usermanager.GetUserId(User))
                        {
                            projectExist.Name = project.Name;
                            projectExist.Description = project.Description;

                            _context.SaveChanges();
                        }
                        else
                        {
                            ModelState.AddModelError("", "You are not Creator of this Project!");
                            return View("ProjectSettings", m);
                        }
                        return View("ProjectSettings", m);
                    }
                    else
                    {
                        return View("Error404");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error404");
            }

        }



        [HttpGet("ProjectSettings/{projectid}")]
        public IActionResult ProjectSettings(int projectid)
        {
            BaseViewModel m = new BaseViewModel();
            Project p = _context.Projects.Include(p => p.ProjectType).FirstOrDefault(p => p.ProjectId == projectid);
            if (p == null)
            {
                return View("Error404");
            }
            else
            {
                m.Project = p;
            }

            var userIds = _context.UserProjects
            .Where(x => x.ProjectId == p.ProjectId)
            .Select(x => x.MemberId)
            .ToList();

            var projectMembers = _context.Users
                .Where(user => userIds.Contains(user.Id))
                .ToList();


            if (!projectMembers.Any(user => user.Id == _usermanager.GetUserId(User)))
            {
                return View("Error404");
            }

            return View("ProjectSettings", m);
        }



        [HttpGet("ProjectMembers/{projectid}")]
        public IActionResult ProjectMembers(int projectid)
        {
            ProjectUsersViewModel m = new ProjectUsersViewModel();
            Project p = _context.Projects.Include(p => p.ProjectType).FirstOrDefault(p => p.ProjectId == projectid);
            if (p == null)
            {
                return View("Error404");
            }

            var memberIds = _context.UserProjects
                .Where(up => up.ProjectId == projectid)
                .Select(up => up.MemberId)
                .ToList();

            var members = _usermanager.Users
                .Where(u => memberIds.Contains(u.Id))
                .ToList();

            if (!members.Any(user => user.Id == _usermanager.GetUserId(User)))
            {
                return View("Error404");
            }

            m.Project = p;
            m.ProjectMembers = members;

            return View(m);

        }



        [HttpPost("AddMember")]
        public IActionResult AddMember(int projectid, string Username)
        {
            try
            {
                if (projectid == 0 || String.IsNullOrEmpty(Username))
                {
                    return Json(new { success = false, message = "Invalid Request, Check Input!" });
                }

                Project p = _context.Projects.Find(projectid);
                if (p == null)
                {
                    return Json(new { success = false, message = "Invalid Project!" });
                }

                if (_usermanager.GetUserId(User) != p.CreatorId)
                {
                    return Json(new { success = false, message = "You Are Not Project Owner! Please contact owner for Addition." });
                }

                IdentityUser member = _usermanager.Users.FirstOrDefault(u => u.NormalizedUserName == Username.ToUpper());
                if (member == null)
                {
                    return Json(new { success = false, message = "User Not Found!" });
                }

                UserProject userProject = new UserProject();
                userProject.Project = p;
                userProject.Member = member;

                _context.UserProjects.Add(userProject);
                _context.SaveChanges();

                return Json(new { success = true, redirectUrl = Url.Action("ProjectMembers", "Project", new { projectid = projectid }) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "An Error Occured While Adding the User. Please Try Again!" });
            }
        }



        [HttpPost("RemoveMember")]
        public IActionResult RemoveMember(int projectid, string memberid)
        {
            try
            {

                if (projectid == 0 || String.IsNullOrEmpty(memberid))
                {
                    return Json(new { success = false, message = "Invalid Request, Check Input!" });
                }

                Project p = _context.Projects.Find(projectid);
                if (p == null)
                {
                    return Json(new { success = false, message = "Invalid Project!" });
                }

                if (_usermanager.GetUserId(User) != p.CreatorId)
                {
                    return Json(new { success = false, message = "You Are Not Project Owner! Please contact owner for Removal." });
                }

                IdentityUser member = _usermanager.Users.FirstOrDefault(u => u.Id == memberid);
                if (member == null)
                {
                    return Json(new { success = false, message = "User Not Found!" });
                }

                UserProject userProject = new UserProject();
                userProject.Project = p;
                userProject.Member = member;

                _context.UserProjects.Remove(userProject);
                _context.SaveChanges();

                return Json(new { success = true, message = member.UserName + " Removed Successfully", redirectUrl = Url.Action("ProjectMembers", "Project", new { projectid = projectid }) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "An Error Occured While Removing the User. Please Try Again!" });
            }
        }



        [HttpDelete("DeleteProjectById/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProjectById(int id)
        {
            var project = _context.Projects.Find(id);

            if (project == null)
            {
                return View("Error404");
            }

            if (_usermanager.GetUserId(User) != project.CreatorId)
            {
                return Json(new { success = false, message = "You Are Not Project Owner! Please contact owner for Deletion." });
            }

            // Perform any additional checks before deletion if needed
            _context.Projects.Remove(project);
            _context.SaveChanges();

            return Json(new { success = true, message = project.Name + " Deleted Successfully." });
        }
    }
}