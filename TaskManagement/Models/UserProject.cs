using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Models
{
    [PrimaryKey(nameof(MemberId), nameof(ProjectId))]
    public class UserProject
    {
        public string MemberId { get; set; }
        [ValidateNever]
        public IdentityUser Member { get; set; }

        public int ProjectId { get; set; }
        [ValidateNever]
        public Project Project { get; set; }

    }
}