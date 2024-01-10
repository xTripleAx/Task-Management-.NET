using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using TaskManagement.Data;
using TaskManagement.Models;
using TaskManagement.Models.ViewModels;

namespace TaskManagement.Controllers
{
    [Authorize]
    [Route("List")]
    public class ListController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ListController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,ColumnLimit,isListForFinish,BoardId")] List newList, int projectid)
        {
            try
            {
                if (projectid == 0)
                {
                    return Json(new { success = false, message = "Project Id Error" });
                }

                if (newList == null)
                {
                    return Json(new { success = false, message = "List Not Found" });
                }

                if (ModelState.IsValid)
                {
                    _context.Lists.Add(newList);
                    _context.SaveChanges();

                    return Json(new { success = true, message = "List Created Successfully", redirectUrl = Url.Action("KanbanBoard", "Board", new { projectid = projectid }) });
                }

                //If ModelState is not valid, return an error message
                return Json(new { success = false, message = "Invalid data. Please check your input." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An Error occured while creating the list." });
            }


        }



        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditList([Bind("ListId,Name,ColumnLimit,isListForFinish,BoardId")] List newList, int projectid)
        {
            try
            {
                // Fetch the existing list from the database
                var existingList = _context.Lists.Find(newList.ListId);

                if (existingList != null)
                {
                    // Update the properties with the new values
                    existingList.Name = newList.Name;
                    existingList.ColumnLimit = newList.ColumnLimit;
                    existingList.isListForFinish = newList.isListForFinish;

                    // Save changes to the database
                    _context.SaveChanges();

                    return Json(new { success = true, message = "List updated successfully", redirectUrl = Url.Action("KanbanBoard", "Board", new { projectid = projectid }) });
                }

                return Json(new { success = false, message = "List not found" });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return Json(new { success = false, message = "An error occurred while updating the list" });
            }
        }



        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int listId, int projectid)
        {
            try
            {
                var existingList = _context.Lists.Find(listId);

                if (existingList != null)
                {
                    // Delete the List
                    _context.Lists.Remove(existingList);

                    // Save changes to the database
                    _context.SaveChanges();

                    return Json(new { success = true, message = "List deleted successfully", redirectUrl = Url.Action("KanbanBoard", "Board", new { projectid = projectid }) });
                }

                return Json(new { success = false, message = "List not found" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while deleting the list" });
            }
        }

    }
}