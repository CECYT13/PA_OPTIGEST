using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PA_ORIGEN_N1.Data;
using PA_ORIGEN_N1.Models;
using PA_ORIGEN_N1.ViewModels;

namespace PA_ORIGEN_N1.Controllers
{
    [Authorize(Roles = "Master")]
    public class MasterController : Controller
    {
        private readonly AplicationDbContext _context;

        public MasterController(AplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Panel()
        {
            return View();
        }

        public IActionResult CrearAdministrador()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearAdministrador(RegistroEmpleadoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = new Usuario
                {
                    Nombre = model.Nombre,
                    Correo = model.Correo,
                    Password = model.Password,
                    Rol = "Administrador", // Se ignora lo que venga del cliente
                    EmailVerificado = true
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                TempData["Mensaje"] = "Administrador creado exitosamente.";
                return RedirectToAction("Panel");
            }

            return View(model);
        }

    }
}
