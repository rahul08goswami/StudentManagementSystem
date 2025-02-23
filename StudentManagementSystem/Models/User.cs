using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Gender { get; set; }

        [StringLength(50, MinimumLength = 10)]
        [Required]
        public string Password { get; set; }

        [StringLength(50, MinimumLength = 10)]
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
