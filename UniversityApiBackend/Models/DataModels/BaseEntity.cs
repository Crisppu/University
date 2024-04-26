using System.ComponentModel.DataAnnotations; //urm

namespace UniversityApiBackend.Models.DataModels
{
    public class BaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string CreateBy { get; set; } = string.Empty;//cuando es una cadena de texto se le agg string.Empty para que comience vacio o bien en el string poner ? (string?) que sig opcional
        public DateTime CreatedAt { get; set; } = DateTime.Now; //cuando se a creado
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedAt { get; set; } //? sera opcional
        public string DeletedBy { get; set; } = string.Empty;
        public DateTime? DeletedAt { get; set; } //? sera opcional

        public bool IsDeleted { get; set; } = false;

    }
}
