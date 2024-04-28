using System.ComponentModel.DataAnnotations;
namespace UniversityApiBackend.Models.DataModels
{
    public enum Level
    {
        Basic,
        Medium,
        Advanced,
        Expert
    }
    public class Course:BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required, StringLength(200)]
        public string ShortDescription { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public Level Level { get; set; } = Level.Basic;

        //la relacion de entidad, 1:N ej
        [Required]
        public ICollection<Category> Categories { get; set; } = new List<Category>();

        [Required]
        public ICollection<Student> Studens { get; set; } = new List<Student>();
        //la relacion de entidad, 1:1 ej

        [Required]
        public Chapter Chaper { get; set; } = new Chapter();

    }
}
