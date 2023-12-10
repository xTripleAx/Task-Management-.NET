using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class ProjectTypes
    {
        [Key]
        public int ProjectTypeId { get; set; }
        public string ProjecTypetName { get; set; }
    }
}
