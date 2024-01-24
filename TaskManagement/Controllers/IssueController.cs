using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO.IsolatedStorage;
using System.Security.Claims;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    [Authorize]
    [Route("Issue")]
    public class IssueController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _usermanager;

        public IssueController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _usermanager = userManager;
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind("IssueName,IssueDescription,AssigneeId,ListId")] Issue newIssue, int projectid)
        {
            try
            {
                if(String.IsNullOrEmpty(newIssue.IssueName) || String.IsNullOrEmpty(newIssue.IssueDescription) || String.IsNullOrEmpty(newIssue.AssigneeId))
                {
                    return Json(new { success = false, message = "Invalid data. Please check your input." });
                }

                Project project = _context.Projects.Find(projectid);

                if (project == null)
                {
                    return Json(new { success = false, message = "Project Not Found" });
                }

                Backlog backlog = _context.Backlogs.FirstOrDefault(b => b.ProjectId == projectid);

                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var userIds = _context.UserProjects
                    .Where(x => x.ProjectId == project.ProjectId)
                    .Select(x => x.MemberId)
                    .ToList();

                var projectMembers = _context.Users
                    .Where(user => userIds.Contains(user.Id))
                    .ToList();


                if (!projectMembers.Any(user => user.Id == _usermanager.GetUserId(User)))
                {
                    return Json(new { success = false, message = "You are not a member of this project!" });
                }


                if (newIssue == null)
                {
                    return Json(new { success = false, message = "Issue Invalid!" });
                }

                newIssue.ReporterId = userid;
                newIssue.DateCreated = DateTime.Now;

                if(newIssue.ListId == 0)
                {
                    newIssue.BacklogId = backlog.BacklogId;
                    newIssue.ListId = null;
                }
                else
                {
                    List l = _context.Lists.Find(newIssue.ListId);
                    IEnumerable<Issue> ListIssues = _context.Issues.Where(I => I.ListId == newIssue.ListId);
                    if(l.ColumnLimit <= ListIssues.Count())
                    {
                        return Json(new { success = false, message = "List Reached Column Limit!" });
                    }
                }

                if (ModelState.IsValid)
                {
                    _context.Issues.Add(newIssue);
                    _context.SaveChanges();

                    return Json(new { success = true, message = "Issue Created Successfully", redirectUrl = Url.Action("KanbanBoard", "Board", new { projectid = projectid }) });
                }

                //If ModelState is not valid, return an error message
                return Json(new { success = false, message = "Invalid data. Please check your input." });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Json(new { success = false, message = "An Error occured while creating the issue." });
            }


        }


        [HttpPost("Edit")]
        public JsonResult Edit([Bind("IssueId,IssueName,IssueDescription,AssigneeId,ListId")] Issue newIssue, int projectid)
        {
            try
            {
                var issue = _context.Issues.FirstOrDefault(l => l.IssueId == newIssue.IssueId);
                var backlog = _context.Backlogs.FirstOrDefault(b => b.ProjectId == projectid);
                if (issue == null || backlog == null)
                {
                    return Json(new { success = false, message = "Issue Not Found" });
                }
                else
                {
                    issue.IssueName = newIssue.IssueName;
                    issue.IssueDescription = newIssue.IssueDescription;
                    issue.AssigneeId = newIssue.AssigneeId;
                    if (issue.ListId == 0)
                    {
                        issue.BacklogId = backlog.BacklogId;
                        issue.ListId = null;
                    }
                    else
                    {
                        issue.ListId = newIssue.ListId;
                        issue.BacklogId = null;
                    }

                    _context.SaveChanges();

                    return Json(new { success = true, message = "Issue updated successfully", redirectUrl = Url.Action("KanbanBoard", "Board", new { projectid = projectid }) });
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Json(new { success = false, message = "An Error occured while editing the issue." });
            }
        }


        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int IssueId, int projectid)
        {
            try
            {
                var Issue = _context.Issues.Find(IssueId);

                if (Issue != null)
                {
                    // Delete the List
                    _context.Issues.Remove(Issue);

                    // Save changes to the database
                    _context.SaveChanges();

                    return Json(new { success = true, message = "Issue deleted successfully", redirectUrl = Url.Action("KanbanBoard", "Board", new { projectid = projectid }) });
                }

                return Json(new { success = false, message = "Issue not found" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while deleting the issue" });
            }
        }

    }
}