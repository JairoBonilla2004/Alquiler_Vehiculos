using System;
using System.Collections.Generic;
using AlquilerVehiculos_DAL;
using AlquilerVehiculos_Entity;

namespace AlquilerVehiculos_BLL
{
    public class SeguroBLL
    {
        private readonly SeguroDAL _seguroDAL;

        public SeguroBLL(SeguroDAL seguroDAL)
        {
            _seguroDAL = seguroDAL;
        }

        // Obtener todos los seguros
        public List<Seguro> ObtenerSeguros()
        {
            return _seguroDAL.GetAll();
        }

        // Obtener un seguro por ID
        public Seguro ObtenerSeguroPorId(int id)
        {
            return _seguroDAL.GetBy("Id", id.ToString());
        }

        // Insertar un nuevo seguro con validaciones
        public object InsertarSeguro(Seguro seguro)
        {
            // Validar que la reserva exista (esto ya se controla con la FK en la BD, pero podemos reforzarlo)
            if (seguro.ReservaId <= 0)
            {
                return new { success = false, message = "Debe seleccionar una reserva válida." };
            }

            // Validar que el tipo de seguro sea válido
            List<string> tiposValidos = new List<string> { "Básico", "Cobertura Total", "Daños a Terceros" };
            if (!tiposValidos.Contains(seguro.TipoSeguro))
            {
                return new { success = false, message = "Tipo de seguro no válido." };
            }

            // Validar que el costo del seguro sea positivo
            if (seguro.Costo <= 0)
            {
                return new { success = false, message = "El costo del seguro debe ser mayor a 0." };
            }

            _seguroDAL.Insertar(seguro);
            return new { success = true, seguro };
        }

        // Actualizar un seguro con validaciones
        public object ModificarSeguro(Seguro seguro)
        {
            if (seguro.Costo <= 0)
            {
                return new { success = false, message = "El costo del seguro debe ser mayor a 0." };
            }

            bool resultado = _seguroDAL.Actualizar(seguro);
            return new { success = resultado };
        }

        // Eliminar un seguro
        public bool EliminarSeguro(int id)
        {
            return _seguroDAL.Eliminar(id);
        }

        // Obtener reservas que aún no tienen seguro
        public List<Reserva> ObtenerReservasSinSeguro()
        {
            return _seguroDAL.ObtenerReservasSinSeguro();
        }
    }
}
