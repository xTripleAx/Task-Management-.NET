using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    //A Type (Board) that holds the lists of the project
    public class Board
    {
        [Key]
        public int BoardId { get; set; }
        public int ProjectId { get; set; }

        [ValidateNever]
        public Project Project { get; set; }
        public ICollection<List> Lists { get; set; }
    }
}