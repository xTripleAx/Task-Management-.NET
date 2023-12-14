using Microsoft.AspNetCore.Mvc;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    public class IssueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IssueController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create(int listid)
        {
            var list = _context.Lists.Find(listid);
            if (list == null)
            {
                return NotFound();
            }
            else
            {
                var issue = new Issue()
                {
                    ListId = listid,
                };
                return View("IssueForm", list);
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Issue issue)
        {

            //Checking the model validity before action
            if (!ModelState.IsValid)
            {
                //if not valid redirect to the form with posted data
                return View("IssueForm", issue);
            }

            //checking if the issue is new or old
            if (issue.IssueId == 0)
            {
                //if new add issue
                _context.Issues.Add(issue);
                _context.SaveChanges();

            }
            else
            {
                //fetch the issue from the database
                var issueExists = _context.Issues.Find(issue.IssueId);

                //if found edit the data
                if (issueExists != null)
                {
                    issueExists.IssueName = issue.IssueName;
                    issueExists.AssigneeId = issue.AssigneeId;
                    issueExists.ReporterId = issue.ReporterId;
                    issueExists.blockedBy = issue.blockedBy;
                    issueExists.blocks = issue.blocks;

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
