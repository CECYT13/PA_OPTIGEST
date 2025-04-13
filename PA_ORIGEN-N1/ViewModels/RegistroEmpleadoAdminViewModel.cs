using System.ComponentModel.DataAnnotations;

namespace PA_ORIGEN_N1.ViewModels
{
    public class RegistroEmpleadoAdminViewModel
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
        [Display(Name = "Tipo de Empleado")]
        public string TipoEmpleado { get; set; } // Optometrista o Vendedor
    }
}

