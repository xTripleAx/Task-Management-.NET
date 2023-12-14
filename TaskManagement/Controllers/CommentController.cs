using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    [Authorize]
    [Route("Comment")]
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("Create/{issueid}")]
        public IActionResult Create(int issueid)
        {
            var issue = _context.Issues.Find(issueid);
            if (issue == null)
            {
                return NotFound();
            }
            else
            {
                var comment = new Comment()
                {
                    IssueId = issueid,
                };
                return View("CommentForm", comment);
            }
        }


        public IActionResult Edit(int commentid, int issueid)
        {
            var comment = _context.Comments.FirstOrDefault(l => l.CommentId == commentid && l.IssueId == issueid);
            if (comment == null)
            {
                return NotFound();
            }

            return View("CommentForm", comment);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Comment comment)
        {

            //Checking the model validity before action
            if (!ModelState.IsValid)
            {
                //if not valid redirect to the form with posted data
                return View("CommentForm", comment);
            }

            //checking if the issue is new or old
            if (comment.CommentId == 0)
            {
                //if new add issue
                _context.Comments.Add(comment);
                _context.SaveChanges();

            }
            else
            {
                //fetch the issue from the database
                var commentExists = _context.Comments.Find(comment.CommentId);

                //if found edit the data
                if (commentExists != null)
                {
                    commentExists.Content = comment.Content;
                    commentExists.IssueId = comment.IssueId;
                    commentExists.CommentorId = comment.CommentorId;
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
