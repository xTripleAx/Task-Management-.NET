using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    //A class for project types that are static in the database
    public class ProjectTypes
    {
        [Key]
        public int ProjectTypeId { get; set; }
        public string ProjecTypetName { get; set; }
    }
}