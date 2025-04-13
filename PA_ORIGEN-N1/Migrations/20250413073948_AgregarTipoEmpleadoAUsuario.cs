using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PA_ORIGEN_N1.Migrations
{
    /// <inheritdoc />
    public partial class AgregarTipoEmpleadoAUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TipoEmpleado",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoEmpleado",
                table: "Usuarios");
        }
    }
}
