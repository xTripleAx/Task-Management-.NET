using Microsoft.AspNetCore.Identity;

namespace TaskManagement.Models
{
    public class Issue
    {
        public int IssueId { get; set; }
        public string IssueName { get; set; }
        public string IssueDescription { get; set; }
        public DateTime DateCreated { get; set; }
        public bool? blocks { get; set; }
        public bool? blockedBy { get; set; }
        public string ReporterId { get; set; }
        public IdentityUser Reporter { get; set; }
        public string? AssigneeId { get; set; }
        public IdentityUser? Assignee { get; set; }
    }
}