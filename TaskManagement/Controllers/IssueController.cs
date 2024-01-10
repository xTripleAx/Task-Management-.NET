using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
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
                if (projectid == 0)
                {
                    return Json(new { success = false, message = "Project Not Found" });
                }

                if (newIssue == null)
                {
                    return Json(new { success = false, message = "Issue Not Found" });
                }

                newIssue.ReporterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
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


        public IActionResult Edit(int issueid, int listid)
        {
            var issue = _context.Issues.FirstOrDefault(l => l.IssueId == issueid && l.ListId == listid);
            if (issue == null)
            {
                return NotFound();
            }

            return View("IssueForm", issue);
        }

    }
}
