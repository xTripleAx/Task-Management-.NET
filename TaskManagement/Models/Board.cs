namespace TaskManagement.Models
{
    //A Type (Board) that holds the lists of the project
    public class Board
    {
        public int BoardId { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}