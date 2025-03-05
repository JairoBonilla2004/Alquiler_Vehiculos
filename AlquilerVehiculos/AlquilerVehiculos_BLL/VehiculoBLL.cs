using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlquilerVehiculos_DAL;
using AlquilerVehiculos_Entity;

namespace AlquilerVehiculos_BLL
{
    public class VehiculoBLL
    {
        public VehiculoDAL _vehiculoDAL;
        public VehiculoBLL(VehiculoDAL vehiculoDAL)
        {
            _vehiculoDAL = vehiculoDAL;
        }

        public List<Vehiculo> obtenerVehiculos()
        {
            return _vehiculoDAL.GetAll();
        }

        public object InsertarVehiculos(Vehiculo vehiculo)
        {
            _vehiculoDAL.Insertar(vehiculo);
            return new { success = true, vehiculo };

        }

        public object EliminarVehiculo(int id)
        {
            _vehiculoDAL.Eliminar(id);
            return new { success = true };
        }

        public object ActualizarVehiculo(Vehiculo vehiculo)
        {
            bool respuesta = _vehiculoDAL.Actualizar(vehiculo);
            if (respuesta)
            {
                return new { success = true };
            }
            else
            {
                return new { success = false };
            }

        }
    }
}
