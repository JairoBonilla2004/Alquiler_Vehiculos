using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlquilerVehiculos_Entity
{
    public class Reserva
    {
        public int Id { get; set; } 
        public int ClienteId { get; set; } // Clave foránea de Clientes
        public int VehiculoId { get; set; } // Clave foránea de Vehiculos
        public DateTime FechaInicio { get; set; } // Fecha de inicio de la reserva
        public DateTime FechaFin { get; set; } // Fecha de finalización de la reserva
        public string Estado { get; set; } // Estado: Pendiente, Confirmada, Cancelada
        public string ClienteNombre { get; set; }
        public string VehiculoNombre { get; set; }
    }
}