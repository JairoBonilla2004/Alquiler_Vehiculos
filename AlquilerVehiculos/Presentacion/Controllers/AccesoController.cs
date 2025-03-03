using AlquilerVehiculos_BLL;
using AlquilerVehiculos_DAL;
using AlquilerVehiculos_Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Presentacion.Controllers
{
    public class AccesoController : Controller
    {


        public EmpleadoBLL _empleadoBLL;
        public AccesoController(EmpleadoBLL empleadoBLL)
        {
            _empleadoBLL = empleadoBLL;
        }
       
        public IActionResult LogIn()
        {
            return View();
        }

        public IActionResult Restablecer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogInPeticion([FromBody] Empleado empleado)
        {

            var result = _empleadoBLL.IniciarSecionEmpleado(empleado.Email, empleado.Clave);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
        [HttpPost]
        public IActionResult Registrar([FromBody] Empleado empleado) {
            var result = _empleadoBLL.RegistrarEmpleado(empleado);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Restablecer([FromBody] Empleado empleado)
        {
            var result = _empleadoBLL.RegistrarEmpleado(empleado);
            return Ok(result);
        }

        public IActionResult Confirmar(string token)//recibe el token del correo 
        {
            _empleadoBLL.ConfirmarCuenta(token);

            return View();
        }

       
    }
}
