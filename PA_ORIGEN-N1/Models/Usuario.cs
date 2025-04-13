using System;
using System.ComponentModel.DataAnnotations;

namespace PA_ORIGEN_N1.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Rol { get; set; } // Cliente, Empleado, Administrador

        // Verificación por correo
        public bool EmailVerificado { get; set; } = false;

        public string? TokenVerificacion { get; set; }
        public bool Activo { get; set; } = false; // por defecto desactivado

        public DateTime? FechaExpiracionToken { get; set; }
        public string? TipoEmpleado { get; set; } // Vendedor u Optometrista

        // Asociación con reportes médicos si es Cliente
        public virtual ICollection<ReporteMedico>? ReportesMedicos { get; set; }
    }
}
