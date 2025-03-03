using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    public class EmpleadoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
