using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Empleado")]
public class VendedorController : Controller
{
    public IActionResult DashboardVendedor()
    {
        return View("~/Views/Empleados/DashboardVendedor.cshtml");
    }
}
