using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PA_ORIGEN_N1.Controllers
{
    [Authorize(Roles = "Empleado")]
    public class OptometristaController : Controller
    {
        public IActionResult DashboardOftalmologo()
        {
            return View("~/Views/Empleados/DashboardOftalmologo.cshtml");
        }
    }
}

