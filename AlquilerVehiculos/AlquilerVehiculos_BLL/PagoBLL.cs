using System;
using System.Collections.Generic;
using AlquilerVehiculos_DAL;
using AlquilerVehiculos_Entity;

namespace AlquilerVehiculos_BLL
{
    public class PagoBLL
    {
        private readonly PagoDAL _pagoDAL;

        public PagoBLL(PagoDAL pagoDAL)
        {
            _pagoDAL = pagoDAL;
        }

        // Obtener todos los pagos
        public List<Pago> ObtenerPagos()
        {
            return _pagoDAL.GetAll();
        }

        // Obtener un pago por ID
        public Pago ObtenerPagoPorId(int id)
        {
            return _pagoDAL.GetBy("Id", id.ToString());
        }

        // Insertar un nuevo pago con validaciones
        public object InsertarPago(Pago pago)
        {
            // Validar que el monto sea positivo
            if (pago.Monto <= 0)
            {
                return new { success = false, message = "El monto del pago debe ser mayor a 0." };
            }

            // Validar que el método de pago sea válido
            List<string> metodosValidos = new List<string> { "Tarjeta", "Efectivo", "Transferencia" };
            if (!metodosValidos.Contains(pago.MetodoPago))
            {
                return new { success = false, message = "Método de pago no válido." };
            }

            _pagoDAL.Insertar(pago);
            return new { success = true, pago };
        }

        // Actualizar un pago existente con validaciones
        public object ModificarPago(Pago pago)
        {
            if (pago.Monto <= 0)
            {
                return new { success = false, message = "El monto del pago debe ser mayor a 0." };
            }

            bool resultado = _pagoDAL.Actualizar(pago);
            return new { success = resultado };
        }

        // Eliminar un pago
        public bool EliminarPago(int id)
        {
            return _pagoDAL.Eliminar(id);
        }
    }
}
