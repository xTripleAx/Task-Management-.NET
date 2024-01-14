using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class Project
    {
        [Key]
        [HiddenInput]
        public int ProjectId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        [ValidateNever]
        public DateTime DateCreated { get; set; }

        [ValidateNever]
        public string CreatorId {  get; set; }

        [ValidateNever]
        public IdentityUser Creator { get; set; }

        [Display(Name = "Project Type")]
        public int ProjectTypeId { get; set; }

        [ValidateNever]
        public ProjectTypes ProjectType { get; set; }
    }
}