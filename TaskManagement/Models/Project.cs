using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? CreatorId {  get; set; }
        public IdentityUser? Creator { get; set; }
        public int ProjectTypeId { get; set; }
        public ProjectTypes? ProjectType { get; set; }
    }
}