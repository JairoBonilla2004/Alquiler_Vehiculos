using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlquilerVehiculos_Entity
{
    public class Vehiculo
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public int Anio { get; set; }
        public string Modelo { get; set; }
        public decimal Precio { get; set; }
        public string Estado { get; set; }
    }
}
