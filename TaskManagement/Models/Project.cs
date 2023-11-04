using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatorId {  get; set; }
        public User Creator { get; set; }

    }
}
