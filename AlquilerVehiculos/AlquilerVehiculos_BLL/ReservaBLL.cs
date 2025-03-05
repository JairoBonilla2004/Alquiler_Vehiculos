using System;
using System.Collections.Generic;
using AlquilerVehiculos_DAL;
using AlquilerVehiculos_Entity;

namespace AlquilerVehiculos_BLL
{
    public class ReservaBLL
    {
        private readonly ReservaDAL _reservaDAL;

        public ReservaBLL(ReservaDAL reservaDAL)
        {
            _reservaDAL = reservaDAL;
        }

        // Obtener todas las reservas con detalles (Cliente y Vehículo)
        public List<ReservaDetalle> ObtenerReservas()
        {
            return _reservaDAL.GetAll();
        }

        // Obtener una reserva específica por ID
        public ReservaDetalle ObtenerReservaPorId(int id)
        {
            return _reservaDAL.GetBy("ReservaId", id.ToString());
        }

        // Insertar una nueva reserva con validaciones
        public object InsertarReserva(Reserva reserva)
        {
            if (reserva.FechaInicio >= reserva.FechaFin)
            {
                return new { success = false, message = "La fecha de inicio debe ser anterior a la fecha de fin." };
            }

            _reservaDAL.Insertar(reserva);
            return new { success = true, reserva };
        }

        // Actualizar una reserva existente con validaciones
        public object ModificarReserva(Reserva reserva)
        {
            if (reserva.FechaInicio >= reserva.FechaFin)
            {
                return new { success = false, message = "La fecha de inicio debe ser anterior a la fecha de fin." };
            }

            bool resultado = _reservaDAL.Actualizar(reserva);
            return new { success = resultado };
        }

        // Eliminar una reserva por ID
        public bool EliminarReserva(int id)
        {
            return _reservaDAL.Eliminar(id);
        }
    }
}