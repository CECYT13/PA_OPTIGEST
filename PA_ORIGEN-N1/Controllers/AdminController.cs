using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PA_ORIGEN_N1.Data;
using PA_ORIGEN_N1.Models;
using PA_ORIGEN_N1.ViewModels;

namespace PA_ORIGEN_N1.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    [Authorize(Roles = "Administrador")]
    public class AdminController : Controller
    {
        private readonly AplicationDbContext _context;

        public AdminController(AplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult CrearEmpleado()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearEmpleado(RegistroEmpleadoAdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                var nuevoEmpleado = new Usuario
                {
                    Nombre = model.Nombre,
                    Correo = model.Correo,
                    Password = model.Password,
                    Rol = "Empleado", // El admin solo puede crear empleados
                    EmailVerificado = true,
                    TipoEmpleado = model.TipoEmpleado
                };

                _context.Usuarios.Add(nuevoEmpleado);
                await _context.SaveChangesAsync();

                TempData["Mensaje"] = "Empleado creado exitosamente.";
                return RedirectToAction("Dashboard");
            }

            return View(model);
        }
    }
}


