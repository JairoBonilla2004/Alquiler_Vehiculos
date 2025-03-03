using AlquilerVehiculos_DAL;
using AlquilerVehiculos_Entity;
using AlquilerVehiculos_Services;
using Azure.Core;
using Microsoft.AspNetCore.Http;

namespace AlquilerVehiculos_BLL
{
    public class EmpleadoBLL
    {

        public EmpleadoDAL _empleadoDAL;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmpleadoBLL(EmpleadoDAL empleadoDAL, IHttpContextAccessor httpContextAccessor) {
            _empleadoDAL = empleadoDAL;
            _httpContextAccessor = httpContextAccessor;
        }

        public Object IniciarSecionEmpleado(string email, string clave)
        {
            Empleado empleado = _empleadoDAL.IniciarSesion(email, UtilsServices.ConvertirASHA256(clave));
            if (empleado != null)
            {
                if (!empleado.Confirmado)//si el empleado no está confirmado, para el mensaje de confirmacion
                {
                    //mensaje de confirmacion para el correo
                    return new { success = false, message = $"El empleado no está confirmado. Por favor, revisa tu correo electrónico. {email}" };


                }
                else if (empleado.Restablecer_clave) {
                    //mensaje de restablecimiento de clave
                    return new { success = false, message = $"Se ha solicitado restableces su cuenta. Porfavor, revise su bandeja del correo {email}" };

                }

            }
            else
            {
                return new { success = false, message = "No se han encontrado credenciales" };
            }
            return new { success = true, redirectUrl = "Home/Index", empleado };
        }

        public Object RegistrarEmpleado(Empleado _empleado)
        {
            Empleado empleado = _empleadoDAL.GetBy("Email", _empleado.Email);
            if (empleado == null)
            {
                // Configurar datos del empleado
                _empleado.Clave = UtilsServices.ConvertirASHA256(_empleado.Clave);
                _empleado.Token = UtilsServices.GenerarToken();
                _empleado.Restablecer_clave = false;
                _empleado.Confirmado = false; // Tiene que confirmar su correo electrónico

                _empleadoDAL.Insertar(_empleado);

                // Obtenemos mi plantilla html de la siguiente parte  wwwroot/templates/
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templatesHTML", "Confirmar_acceso.html");

                if (File.Exists(path))
                {
              
                    string contenido = File.ReadAllText(path);
                    string dominio = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";//dominio actual
                    string redirect = "Acceso/Confirmar";
                    string linkConfirmacion = $"{dominio}/{redirect}?token={_empleado.Token}";
                    contenido = contenido.Replace("{0}", _empleado.Nombre).Replace("{1}", linkConfirmacion);//ES ELLINK QUE YO CAMBIO QUE VA A TENER ESE HTML PARA TRAERME DEL CORREO A MI PÁWINA

             
                    Correo correo = new Correo
                    {
                        Para = _empleado.Email,
                        Asunto = "Confirma tu correo electrónico",
                        Contenido = contenido
                    };

                
                    bool enviado = EmailServices.Enviar(correo);
                    if (!enviado)
                    {
                        return new { success = false, message = "No se pudo enviar el correo de confirmación" };
                    }
                }
                else
                {
                    return new { success = false, message = "No se encontró la plantilla de correo" };
                }

                return new { success = true, message = $"Registro exitoso. Revisa tu correo {_empleado.Email}  para confirmarlo" };
            }
            else
            {
                return new { success = false, message = "El correo ya se encuentra registrado" };
            }
        }


        public object ConfirmarCuenta(string token)
        {
            bool response =  _empleadoDAL.Confirmar(token);
            return new { success = response, message = "Su cuenta ya fue confirmada" };
        }

        public object RecuperarClave(string correo)
        {
            Empleado empleado = _empleadoDAL.GetBy("Email", correo);
            if (empleado != null) 
            {
                bool response = _empleadoDAL.RestablecerActualizar(1, empleado.Clave, empleado.Token);
                if (response) {
                    return new { success = response, message = "Contraseña " };
                }
                else
                {
                    return new { success = response, menssage = "" };
                }
            }
            else
            {
                return new { success = false, message = "No se encontraron coincidencias con el correo electrónico" };
            }

        }
    }
}
