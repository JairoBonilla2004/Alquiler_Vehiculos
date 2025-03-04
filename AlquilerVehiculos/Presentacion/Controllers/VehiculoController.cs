using AlquilerVehiculos_BLL;
using AlquilerVehiculos_Entity;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.Controllers
{
    public class VehiculoController : Controller
    {
        public VehiculoBLL _vehiculoBLL;
        public VehiculoController(VehiculoBLL vehiculoBLL)
        {
            _vehiculoBLL = vehiculoBLL;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ObtenerVehiculos()
        {
           var resultado = _vehiculoBLL.obtenerVehiculos();
            return Ok(resultado);
        }

        [HttpPost]
        public IActionResult InsertarDatos([FromBody] Vehiculo vehiculo)
        {
            Console.WriteLine(vehiculo);
            var result = _vehiculoBLL.InsertarVehiculos(vehiculo);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult ActualizarDatos([FromBody] Vehiculo vehiculo)
        {
            Console.WriteLine(vehiculo);
            var result = _vehiculoBLL.ActualizarVehiculo(vehiculo);
            return Ok(result);
        }

        [HttpDelete("Vehiculo/EliminarDatos/{id}")]
        public IActionResult EliminarDatos(int id)
        {
            Console.WriteLine(id);
            var response = _vehiculoBLL.EliminarVehiculo(id);
            return Ok(response);
        }
    }
}
