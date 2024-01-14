using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public IssueController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind("IssueName,IssueDescription,AssigneeId,ListId")] Issue newIssue, int projectid)
        {
            try
            {

                Project project = _context.Projects.Find(projectid);

                if (project == null)
                {
                    return Json(new { success = false, message = "Project Not Found" });
                }

                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);


                if (newIssue == null)
                {
                    return Json(new { success = false, message = "Issue Not Found" });
                }

                newIssue.ReporterId = userid;
                newIssue.DateCreated = DateTime.Now;

                Console.WriteLine(newIssue.ReporterId);

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
                return Json(new { success = false, message = "An Error occured while creating the issue." });
            }


        }


        [HttpPost("Edit")]
        public JsonResult Edit([Bind("IssueId,IssueName,IssueDescription,AssigneeId,ListId")] Issue newIssue, int projectid)
        {
            try
            {
                var issue = _context.Issues.FirstOrDefault(l => l.IssueId == newIssue.IssueId);
                if (issue == null)
                {
                    return Json(new { success = false, message = "Issue Not Found" });
                }
                else
                {
                    issue.IssueName = newIssue.IssueName;
                    issue.IssueDescription = newIssue.IssueDescription;
                    issue.AssigneeId = newIssue.AssigneeId;
                    issue.ListId = newIssue.ListId;

                    _context.SaveChanges();

                    return Json(new { success = true, message = "Issue updated successfully", redirectUrl = Url.Action("KanbanBoard", "Board", new { projectid = projectid }) });
                }
            }
            catch(Exception ex)
            {
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