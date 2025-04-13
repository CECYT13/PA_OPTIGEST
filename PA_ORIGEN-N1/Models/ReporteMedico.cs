using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PA_ORIGEN_N1.Models
{
    public class ReporteMedico
    {
        [Key]
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaGeneracion { get; set; } = DateTime.Now;

        public string ArchivoRuta { get; set; } // Ruta del archivo PDF generado

        // Relación con el cliente
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
