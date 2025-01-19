using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{

    public class Student
    {
        [Display(Name = "Registration No")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public int Age { get; set; }
    }

}
