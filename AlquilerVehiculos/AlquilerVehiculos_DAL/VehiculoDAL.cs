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
    public class VehiculoDAL : IRepository<Vehiculo>
    {

        private string _conexion;
        public VehiculoDAL(Conexion conexion)
        {
            _conexion = conexion.GetConeccion();
        }
        public bool Actualizar(Vehiculo entidad)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_conexion))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("ActualizarVehiculo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = entidad.Id;
                        command.Parameters.Add(new SqlParameter("@Marca", SqlDbType.NVarChar, 50)).Value = entidad.Marca;
                        command.Parameters.Add(new SqlParameter("@Modelo", SqlDbType.NVarChar, 50)).Value = entidad.Modelo;
                        command.Parameters.Add(new SqlParameter("@Anio", SqlDbType.Int)).Value = entidad.Anio;
                        command.Parameters.Add(new SqlParameter("@Precio", SqlDbType.Decimal)).Value = entidad.Precio;
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de error
                Console.WriteLine("Error al actualizar vehículo: " + ex.Message);
                return false;
            }
        }


        public bool Eliminar(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_conexion))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("EliminarVehiculo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int)).Value = id;
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar vehículo: " + ex.Message);
                return false;
            }
        }

        public List<Vehiculo> GetAll()
        {
            List<Vehiculo> vehiculos = new List<Vehiculo>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_conexion))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("ObtenerVehiculos", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                vehiculos.Add(new Vehiculo
                                {
                                    Id = reader.GetInt32(0),
                                    Marca = reader.GetString(1),
                                    Modelo = reader.GetString(2),
                                    Anio = reader.GetInt32(3),
                                    Precio = reader.GetDecimal(4),
                                    Estado = reader.GetString(5)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error en la base de datos: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
            }
            return vehiculos;
        }

        public List<Vehiculo> GetAllByCharacters(string variable)
        {
            throw new NotImplementedException();
        }

        public Vehiculo GetBy(string campo, string valor)
        {
            throw new NotImplementedException();
        }

        public void Insertar(Vehiculo entidad)
        {

            try
            {
                using (SqlConnection con = new SqlConnection(_conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_InsertarVehiculo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Marca", entidad.Marca);
                        cmd.Parameters.AddWithValue("@Modelo", entidad.Modelo);
                        cmd.Parameters.AddWithValue("@Anio", entidad.Anio);
                        cmd.Parameters.AddWithValue("@Precio", entidad.Precio);
                        cmd.Parameters.AddWithValue("@Estado", "Disponible");
                        con.Open();
                        cmd.ExecuteNonQuery();

                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Error SQL: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }
    }
}
