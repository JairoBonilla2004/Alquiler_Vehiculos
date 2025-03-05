using AlquilerVehiculos_BLL;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    public class EmpleadoController : Controller
    {
        public EmpleadoBLL _empleadoBLL;
        public EmpleadoController(EmpleadoBLL empleadoBll) { 
            _empleadoBLL = empleadoBll;
        
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ObtenerEmpleados()
        {
            var result = _empleadoBLL.ObtenerEmpleadosFormulario();
            return Ok(result);
        }
    }
}
