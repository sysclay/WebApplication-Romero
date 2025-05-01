using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_Romero.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public required string nombre { get; set; }
        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        public required string telefono { get; set; }
        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Correo inválido.")]
        public required string correo { get; set; }
        [Required(ErrorMessage = "El nombre de la compañía es obligatorio.")]
        public required string nombre_company { get; set; }
        [Required(ErrorMessage = "La calle es obligatoria.")]
        public required string calle { get; set; }
        [Required(ErrorMessage = "La latitud es obligatoria.")]
        public required string latitud { get; set; }
        [Required(ErrorMessage = "La longitud es obligatoria.")]
        public required string longitud { get; set; }
    }
}
