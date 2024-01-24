using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;

namespace TaskManagement.Controllers.api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IdentityUser>>> GetAllUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            IQueryable<IdentityUser> UsersQuery = _context.Users;

            return await UsersQuery.ToListAsync();
        }
    }
}