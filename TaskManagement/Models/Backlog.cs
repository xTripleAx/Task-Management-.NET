using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class Backlog
    {
        [Key]
        public int BacklogId { get; set; }
        public int ProjectId { get; set; }

        [ValidateNever]
        public Project Project { get; set; }
        public ICollection<Sprint>? Sprints { get; set; }
    }
}
