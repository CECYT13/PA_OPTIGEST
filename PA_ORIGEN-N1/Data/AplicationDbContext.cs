using Microsoft.EntityFrameworkCore;
using PA_ORIGEN_N1.Models;  // Asegúrate de que el namespace de tus modelos coincida

namespace PA_ORIGEN_N1.Data  // Cambié el espacio de nombres a PA_ORIGEN_N1
{
    public class AplicationDbContext : DbContext
    {
        // Constructor que pasa las opciones de configuración al DbContext base
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
            : base(options)
        { }

        // Representa las tablas de tu base de datos. Agrega las entidades correspondientes.
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ReporteMedico> ReportesMedicos { get; set; }
        // public DbSet<Producto> Productos { get; set; }
        // Agrega aquí otras entidades que vayas a utilizar en tu base de datos, por ejemplo:
        // public DbSet<Factura> Facturas { get; set; }
    }
}
