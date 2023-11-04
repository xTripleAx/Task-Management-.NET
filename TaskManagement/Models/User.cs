using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [StringLength(100)]
        public string Username { get; set; }
        [StringLength(100)]
        public string Password { get; set; }
        [StringLength(100)]
        public string PasswordSalt { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(50)]
        public string? PhoneNumber { get; set; }

    }
}
