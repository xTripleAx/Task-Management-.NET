using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ServiceStack;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class List
    {
        [Key]
        public int ListId { get; set; }
        public string Name { get; set; }
        public int ColumnLimit { get; set; }
        public bool isListForFinish { get; set; }
        public int BoardId { get; set; }
        [ValidateNever]
        public Board Board { get; set; }
        public ICollection<Issue> Issues { get; set; }
    }
}