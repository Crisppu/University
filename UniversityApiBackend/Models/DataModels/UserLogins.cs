using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class UserLogins
    {
        //ojo en la Controllers no se uso Entity franword sino mas bien  - Controlador de api en blanco
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
