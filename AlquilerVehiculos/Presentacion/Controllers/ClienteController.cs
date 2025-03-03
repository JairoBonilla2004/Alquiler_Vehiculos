using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
