using AlquilerVehiculos_BLL;
using AlquilerVehiculos_DAL;
using AlquilerVehiculos_Entity;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    public class PagoController : Controller
    {
        private readonly PagoBLL _pagoBLL;

        public PagoController(PagoBLL pagoBLL)
        {
            _pagoBLL = pagoBLL;
        }

        // Página principal de Pagos
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ObtenerPagos()
        {
            var resultado = _pagoBLL.ObtenerPagos();
            return Ok(resultado);
        }

        [HttpGet]
        public IActionResult ObtenerPago(int id)
        {
            var resultado = _pagoBLL.ObtenerPagoPorId(id);
            if (resultado == null)
                return NotFound(new { success = false, message = "Pago no encontrado" });

            return Ok(resultado);
        }

        [HttpPost]
        public IActionResult InsertarDatos([FromBody] Pago pago)
        {
            var resultado = _pagoBLL.InsertarPago(pago);
            return Ok(resultado);
        }

        // Actualizar un pago
        [HttpPost]
        public IActionResult ActualizarDatos([FromBody] Pago pago)
        {
            var resultado = _pagoBLL.ModificarPago(pago);
            return Ok(resultado);
        }

        // Eliminar un pago por ID
        [HttpDelete("Pago/EliminarDatos/{id}")]
        public IActionResult EliminarDatos(int id)
        {
            var resultado = _pagoBLL.EliminarPago(id);
            return Ok(new { success = resultado });
        }
    }
}
