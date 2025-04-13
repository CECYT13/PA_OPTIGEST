// AccesoController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using PA_ORIGEN_N1.Data;
using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;
using PA_ORIGEN_N1.Helpers;
using PA_ORIGEN_N1.Models;


namespace PA_ORIGEN_N1.Controllers
{
    public class AccesoController : Controller
    {
        private readonly AplicationDbContext _context;

        public AccesoController(AplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string correo, string contraseña)
        {
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contraseña))
            {
                ViewBag.Error = "Debe ingresar correo y contraseña.";
                return View("Login");
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo.Trim() == correo.Trim() && u.Password.Trim() == contraseña.Trim());

            if (usuario == null)
            {
                ViewBag.Error = "Credenciales inválidas";
                return View("Login");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.Rol.Trim()),
                new Claim("Nombre", usuario.Nombre)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Redirección según rol
            if (usuario.Rol == "Master")
                return RedirectToAction("Panel", "Master");

            if (usuario.Rol == "Administrador")
                return RedirectToAction("Dashboard", "Admin");

            if (usuario.Rol == "Empleado")
            {
                if (usuario.TipoEmpleado == "Vendedor")
                    return RedirectToAction("DashboardVendedor", "Vendedor");

                if (usuario.TipoEmpleado == "Optometrista")
                    return RedirectToAction("DashboardOftalmologo", "Optometrista");

                return RedirectToAction("Inicio", "Empleado");
            }

            if (usuario.Rol == "Cliente")
                return RedirectToAction("Inicio", "Cliente");

            return RedirectToAction("Login");
        }



        // GET: /Acceso/Registrarse
        [HttpGet]
        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(string correo, string nombre, string contraseña)
        {
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(contraseña))
            {
                ViewBag.Error = "Todos los campos son obligatorios.";
                return View();
            }

            var existe = await _context.Usuarios.AnyAsync(u => u.Correo == correo);
            if (existe)
            {
                ViewBag.Error = "El correo ya está registrado.";
                return View();
            }

            var token = Guid.NewGuid().ToString();

            var nuevo = new Usuario
            {
                Correo = correo.Trim(),
                Nombre = nombre.Trim(),
                Password = contraseña.Trim(),
                Rol = "Cliente",
                Activo = false,
                TokenVerificacion = token
            };

            _context.Usuarios.Add(nuevo);
            await _context.SaveChangesAsync();

            // Enviar correo usando el helper
            var apiKey = "TU_API_KEY_DE_SENDGRID"; // ⚠️ Reemplaza con tu clave real o usa IConfiguration
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var correoHelper = new CorreoHelper(apiKey);
            await correoHelper.EnviarVerificacionAsync(nuevo.Correo, nuevo.Nombre, token, baseUrl);

            return View("ConfirmacionRegistro");
        }

        public async Task<IActionResult> VerificarCorreo(string token)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.TokenVerificacion == token);

            if (usuario == null)
            {
                ViewBag.Error = "El enlace de verificación no es válido o ha expirado.";
                return View("Error");
            }

            usuario.Activo = true;
            usuario.TokenVerificacion = null;
            await _context.SaveChangesAsync();

            ViewBag.Mensaje = "¡Cuenta activada correctamente! Ya puedes iniciar sesión.";
            return View("ConfirmacionActivacion");
        }

    }
}


