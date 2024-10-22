using System.ComponentModel.DataAnnotations;

namespace VBOOK2.Backend.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        public string Role { get; set; }
        public string ProfileImage { get; set; }
    }
}
