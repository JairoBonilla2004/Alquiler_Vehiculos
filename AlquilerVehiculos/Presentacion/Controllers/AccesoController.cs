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
        public IActionResult PeticionRestablecer([FromBody] Empleado empleado)
        {
            if(empleado != null)
            {
                var result = _empleadoBLL.RecuperarClave(empleado.Email);
                return Ok(result);
            }
            else
            {
                return BadRequest(new { status = false, message = "El empleado es null" });
            }
           
        }
  
        public IActionResult Restablecer()// primer Ingreso del email para restablecer la clave
        {
            return View();
        }

        public IActionResult RestablecerForm(string token)
        {
            ViewBag.Token = token;
            
            return View();
        }

        public IActionResult ActualizarClave([FromBody] Empleado empleado)
        {
            var response =  _empleadoBLL.ActualizarClave(empleado.Token, empleado.Clave);
            return Ok(response);
        }


        public IActionResult Confirmar(string token)//recibe el token del correo 
        {
            _empleadoBLL.ConfirmarCuenta(token);

            return View();
        }

       
    }
}
