using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class Sprint
    {
        [Key]
        public int SprintId { get; set; }
        public string SprintName { get; set; }
        public bool isActive { get; set; }
        public DateTime SprintStart { get; set; }
        public DateTime SprintEnd { get; set;}
        public int BacklogId { get; set; }

        [ValidateNever]
        public Backlog Backlog { get; set; }
    }
}