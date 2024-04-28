using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Student : BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;
        
        //Dob = fehca de nacimiento
        [Required]
        public DateTime Dob { get; set; }

        [Required]
        public ICollection<Course> Course { get; set; } = new List<Course>();
    }
}
