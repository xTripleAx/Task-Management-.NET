using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Models;

namespace TaskManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users {  get; set; }
        public DbSet<Project> Projects { get; set; }

        public DbSet<Kanban> KanbanProjects { get; set;}
        public DbSet<Scrum> scrums { get; set; }
    }
}