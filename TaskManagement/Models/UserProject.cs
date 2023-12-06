using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class UserProject
    {
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        [Key]
        public string CompositeKey { get; set; }
    }
}