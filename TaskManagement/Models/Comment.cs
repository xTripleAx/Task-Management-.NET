using Microsoft.AspNetCore.Identity;

namespace TaskManagement.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public int IssueId { get; set; }
        public Issue Issue { get; set; }
        public string CommentorId { get; set; }
        public IdentityUser Commentor { get; set; }
        public DateTime PostedAt { get; set; }
    }

}
