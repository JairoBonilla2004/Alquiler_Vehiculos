using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlquilerVehiculos_DAL;
using AlquilerVehiculos_Entity;

namespace AlquilerVehiculos_BLL
{
    public class ClienteBLL
    {
        public AlquilerVehiculos_DAL.ClienteDAL _clienteDAL;
        public ClienteBLL(AlquilerVehiculos_DAL.ClienteDAL clienteDAL)
        {
            _clienteDAL = clienteDAL;
        }

        public List<Cliente> obtenerCliente()
        {
            return _clienteDAL.GetAll();
        }

        public object InsertarClientes(Cliente cliente)
        {
            bool respuesta = _clienteDAL.Insertar(cliente);
            if (respuesta)
            {
                return new { success = true, message = "Cliente ingresado con éxito" };
            }
            else
            {
                return new { success = false, message = "verifique que los correos sean distintos" };
            }

        }

        public object EliminarCliente(int id)
        {
            _clienteDAL.Eliminar(id);
            return new { success = true };
        }

        public object ActualizarCliente(Cliente cliente)
        {
            bool respuesta = _clienteDAL.Actualizar(cliente);
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
