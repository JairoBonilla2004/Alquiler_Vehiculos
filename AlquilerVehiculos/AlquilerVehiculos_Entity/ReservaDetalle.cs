using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlquilerVehiculos_Entity
{
    public class ReservaDetalle
    {
        public int Id { get; set; }
        public string ClienteNombre { get; set; } // Nombre del Cliente
        public string VehiculoNombre { get; set; } // Marca + Modelo del Vehículo
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Estado { get; set; }
    }
}