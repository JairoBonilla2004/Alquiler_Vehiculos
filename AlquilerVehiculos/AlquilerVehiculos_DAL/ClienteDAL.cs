using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlquilerVehiculos_Entity;
using Microsoft.Data.SqlClient;
using Sistema.DAL;

namespace AlquilerVehiculos_DAL
{
    public class ClienteDAL 
    {
        public string _connectionString;
        public ClienteDAL(Conexion conexion) {
            _connectionString = conexion.GetConeccion();
        }

        public List<Cliente> GetAll()
        {
            List<Cliente> clientes = new List<Cliente>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("ObtenerClientes", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clientes.Add(new Cliente
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Apellido = reader["Apellido"].ToString(),
                                    Telefono = reader["Telefono"].ToString(),
                                    Email = reader["Email"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener clientes: " + ex.Message);
            }

            return clientes;
        }

        // Insertar un cliente
        public bool Insertar(Cliente entidad)
        {
            Console.WriteLine(entidad);
            bool respuesta = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("InsertarCliente", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros con tipos explícitos
                        cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar, 100)).Value = entidad.Nombre;
                        cmd.Parameters.Add(new SqlParameter("@Apellido", SqlDbType.NVarChar, 100)).Value = entidad.Apellido;
                        cmd.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.NVarChar, 20)).Value = entidad.Telefono;
                        cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 200)).Value = entidad.Email;

                        int filas = cmd.ExecuteNonQuery();
                        respuesta = filas > 0;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error SQL al insertar cliente: {sqlEx.Message}");
                // Aquí podrías loguearlo en un archivo o una base de datos.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general al insertar cliente: {ex.Message}");
            }

            return respuesta;
        }


        // Actualizar un cliente
        public bool Actualizar(Cliente entidad)
        {
            bool actualizado = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("ActualizarCliente", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", entidad.Id);
                        cmd.Parameters.AddWithValue("@Nombre", entidad.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", entidad.Apellido);
                        cmd.Parameters.AddWithValue("@Telefono", entidad.Telefono);
                        cmd.Parameters.AddWithValue("@Email", entidad.Email);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        actualizado = filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar cliente: " + ex.Message);
            }

            return actualizado;
        }

        // Eliminar un cliente
        public bool Eliminar(int id)
        {
            bool eliminado = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("EliminarCliente", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        eliminado = filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar cliente: " + ex.Message);
            }

            return eliminado;
        }


       
        
    }
}
