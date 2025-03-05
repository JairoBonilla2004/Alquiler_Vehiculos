using AlquilerVehiculos_BLL;
using AlquilerVehiculos_Entity;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    public class ReservaController : Controller
    {
        private readonly ReservaBLL _reservaBLL;

        public ReservaController(ReservaBLL reservaBLL)
        {
            _reservaBLL = reservaBLL;
        }

        // Página principal de Reservas
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ObtenerReservas()
        {
            var resultado = _reservaBLL.ObtenerReservas();
            return Ok(resultado);
        }

        [HttpGet("Reserva/ObtenerReserva/{id}")]
        public IActionResult ObtenerReserva(int id)
        {
            var resultado = _reservaBLL.ObtenerReservaPorId(id);
            if (resultado == null)
                return NotFound(new { success = false, message = "Reserva no encontrada" });

            return Ok(resultado);
        }


        [HttpPost]
        public IActionResult InsertarDatos([FromBody] Reserva reserva)
        {
            var resultado = _reservaBLL.InsertarReserva(reserva);
            return Ok(resultado);
        }

        [HttpPost]
        public IActionResult ActualizarDatos([FromBody] Reserva reserva)
        {
            var resultado = _reservaBLL.ModificarReserva(reserva);
            return Ok(resultado);
        }

        [HttpDelete("Reserva/EliminarDatos/{id}")]
        public IActionResult EliminarDatos(int id)
        {
            var resultado = _reservaBLL.EliminarReserva(id);
            return Ok(new { success = resultado });
        }
    }
}