using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string Content { get; set; }
        public int IssueId { get; set; }

        [ValidateNever]
        public Issue Issue { get; set; }
        public string CommentorId { get; set; }

        [ValidateNever]
        public IdentityUser Commentor { get; set; }
        public DateTime? PostedAt { get; set; }
    }

}