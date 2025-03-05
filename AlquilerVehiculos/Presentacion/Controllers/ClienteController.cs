using AlquilerVehiculos_BLL;
using AlquilerVehiculos_Entity;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    public class ClienteController : Controller
    {
        public ClienteBLL _clienteBLL;
        public ClienteController(ClienteBLL clienteBLL)
        {
            _clienteBLL = clienteBLL;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ObtenerDatos()
        {
            var resultado = _clienteBLL.obtenerCliente();
            return Ok(resultado);
        }

        [HttpPost]
        public IActionResult InsertarDatos([FromBody] Cliente cliente)
        {
            var result = _clienteBLL.InsertarClientes(cliente);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult ActualizarDatos([FromBody] Cliente vehiculo)
        {
            Console.WriteLine(vehiculo);
            var result = _clienteBLL.ActualizarCliente(vehiculo);
            return Ok(result);
        }

        [HttpDelete("Cliente/EliminarDatos/{id}")]
        public IActionResult EliminarDatos(int id)
        {
            Console.WriteLine(id);
            var response = _clienteBLL.EliminarCliente(id);
            return Ok(response);
        }
    }
}
