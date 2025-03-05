using AlquilerVehiculos_BLL;
using AlquilerVehiculos_DAL;
using AlquilerVehiculos_Entity;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    public class SeguroController : Controller
    {
        private readonly SeguroBLL _seguroBLL;

        public SeguroController(SeguroBLL seguroBLL)
        {
            _seguroBLL = seguroBLL;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ObtenerSeguros()
        {
            var resultado = _seguroBLL.ObtenerSeguros();
            return Ok(resultado);
        }

        [HttpGet]
        public IActionResult ObtenerSeguro(int id)
        {
            var resultado = _seguroBLL.ObtenerSeguroPorId(id);
            if (resultado == null)
                return NotFound(new { success = false, message = "Seguro no encontrado" });

            return Ok(resultado);
        }

        [HttpPost]
        public IActionResult InsertarDatos([FromBody] Seguro seguro)
        {
            var resultado = _seguroBLL.InsertarSeguro(seguro);
            return Ok(resultado);
        }

        [HttpPost]
        public IActionResult ActualizarDatos([FromBody] Seguro seguro)
        {
            var resultado = _seguroBLL.ModificarSeguro(seguro);
            return Ok(resultado);
        }

        [HttpDelete("Seguro/EliminarDatos/{id}")]
        public IActionResult EliminarDatos(int id)
        {
            var resultado = _seguroBLL.EliminarSeguro(id);
            return Ok(new { success = resultado });
        }

        [HttpGet]
        public IActionResult ObtenerReservasSinSeguro()
        {
            var resultado = _seguroBLL.ObtenerReservasSinSeguro();
            return Ok(resultado);
        }
    }
}
