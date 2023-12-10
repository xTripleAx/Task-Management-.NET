using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class Backlog
    {
        [Key]
        public int BacklogId { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }

    }
}
