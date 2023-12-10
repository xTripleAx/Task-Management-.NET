using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class Epic
    {
        [Key]
        public int EpicId { get; set; }
        public string EpicName { get; set; }
        public int BoardId { get; set; }
        public Board Board { get; set; }
    }
}
