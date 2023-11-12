namespace TaskManagement.Models
{
    public class Epic
    {
        public int EpicId { get; set; }
        public string EpicName { get; set; }
        public int BoardId { get; set; }
        public Board Board { get; set; }
    }
}
