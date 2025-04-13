using System.ComponentModel.DataAnnotations;

namespace PA_ORIGEN_N1.ViewModels
{
    public class RegistroEmpleadoViewModel
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarPassword { get; set; }

        [Required]
        [Display(Name = "Tipo de Usuario")]
        public string Rol { get; set; } // "Administrador" o "Empleado"
    }
}
