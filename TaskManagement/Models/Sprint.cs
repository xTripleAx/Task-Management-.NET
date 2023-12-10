using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class Sprint
    {
        [Key]
        public int SprintId { get; set; }
        public string SprintName { get; set; }
        public DateTime SprintStart { get; set; }
        public DateTime SprintEnd { get; set;}
        public int BacklogId { get; set; }
        public Backlog Backlog { get; set; }
    }
}