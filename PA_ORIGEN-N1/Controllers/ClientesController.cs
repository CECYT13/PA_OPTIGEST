using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PA_ORIGEN_N1.Controllers
{
    [Authorize(Roles = "Cliente")]
    public class ClientesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
