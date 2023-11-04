namespace TaskManagement.Models
{
    public class List
    {
        public int ListId { get; set; }
        public string Name { get; set; }
        public int ColumnLimit { get; set; }
        public int BoardId { get; set; }
        public Board Board { get; set; }
    }
}