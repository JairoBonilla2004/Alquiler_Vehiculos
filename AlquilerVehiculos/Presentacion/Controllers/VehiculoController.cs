using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    public class VehiculoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
