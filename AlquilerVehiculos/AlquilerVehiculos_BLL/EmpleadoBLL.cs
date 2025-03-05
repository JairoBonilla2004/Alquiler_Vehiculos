using System.IO;
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

        public EmpleadoBLL(EmpleadoDAL empleadoDAL, IHttpContextAccessor httpContextAccessor)
        {
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
                else if (empleado.Restablecer_clave)
                {
                    //mensaje de restablecimiento de clave
                    return new { success = false, message = $"Se ha solicitado restableces su cuenta. Porfavor, revise su bandeja del correo {email}" };

                }

            }
            else
            {
                return new { success = false, message = "No se han encontrado credenciales" };
            }
            return new { success = true, redirectUrl = "Vehiculo/Index", empleado };
        }

        public bool enviarCorreo(string path, string redirect, Empleado _empleado, string _asunto)
        {
            string contenido = File.ReadAllText(path);
            string dominio = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";//dominio actual
            string linkConfirmacion = $"{dominio}/{redirect}?token={_empleado.Token}";
            contenido = contenido.Replace("{0}", _empleado.Nombre).Replace("{1}", linkConfirmacion);                                                          //ES ELLINK QUE YO CAMBIO QUE VA A TENER ESE HTML PARA TRAERME DEL CORREO A MI PÁWINA
            Correo correo = new Correo
            {
                Para = _empleado.Email,
                Asunto = _asunto,
                Contenido = contenido
            };


            return EmailServices.Enviar(correo);
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
                    bool enviado = enviarCorreo(path, "Acceso/Confirmar", _empleado, "Confirma tu correo electrónico");
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
            bool response = _empleadoDAL.Confirmar(token);
            return new { success = response, message = "Su cuenta ya fue confirmada" };
        }

        public object RecuperarClave(string correo)
        {
            Empleado empleado = _empleadoDAL.GetBy("Email", correo);
            Console.WriteLine(correo, empleado);
            if (empleado != null)
            {
                bool response = _empleadoDAL.RestablecerActualizar(1, empleado.Clave, empleado.Token);//solo necesito que me cambie a uno
                if (response)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templatesHTML", "Restablecer.html");
                    if (File.Exists(path))
                    {
                        bool enviar_correo = enviarCorreo(path, "Acceso/RestablecerForm", empleado, "Restablecer Clave");
                        if (enviar_correo)
                        {
                            return new { success = true, title = "¡Enlace enviado!", message = $"Hemos enviado un enlace de recuperación a {correo}. Por favor revise su bandeja de entrada." };
                        }
                        else
                        {
                            return new { success = false, message = "No se puedo enviar el correo electrónico" };
                        }
                    }
                    else
                    {
                        return new { success = false, message = "No se encontró la plantilla de correo" };
                    }


                }
                else
                {
                    return new { success = response, menssage = "No se pudo restablecer la contraseña" };
                }
            }
            else
            {
                return new { success = false, message = "No se encontraron coincidencias con el correo electrónico", title= "Error al enviar el correo" };
            }

        }

        public object ActualizarClave(string token, string clave)
        {
            bool response = _empleadoDAL.RestablecerActualizar(0, UtilsServices.ConvertirASHA256(clave), token);
            if (response) {
                return new { success = true, title = "¡Contraseña actualizada!", message = "Su contraseña ha sido restablecida exitosamente. Ahora puede iniciar sesión con su nueva contraseña." };
            }
            else
            {
                return new { success = false, title = "No se ha podido actualizar la contraseña", message = "No se ha podido actualizar la contraseña contáctese con administracion@gmail.com" };
            }
        }

        public object ObtenerEmpleadosFormulario()
        {
            var resultado = _empleadoDAL.GetAll();
            if (resultado == null)
            {
                return new { success = false, message = "No se pudo extraer los empleados" };
            }
            else
            {
                return resultado;
            }
        }
    }
}
