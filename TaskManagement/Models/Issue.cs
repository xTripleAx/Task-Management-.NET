﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class Issue
    {
        [Key]
        public int IssueId { get; set; }
        public string IssueName { get; set; }
        public string IssueDescription { get; set; }
        public DateTime DateCreated { get; set; }
        public bool? blocks { get; set; }
        public bool? blockedBy { get; set; }

        [ValidateNever]
        public string ReporterId { get; set; }
        [ValidateNever]
        public IdentityUser Reporter { get; set; }

        public int? ListId { get; set; }
        [ValidateNever]
        public List List {  get; set; }

        public int? BacklogId { get; set; }
        [ValidateNever]
        public Backlog Backlog { get; set; }

        public string? AssigneeId { get; set; }
        [ValidateNever]
        public IdentityUser? Assignee { get; set; }

        [ValidateNever]
        public ICollection<Comment> Comments { get; set; }
    }
}