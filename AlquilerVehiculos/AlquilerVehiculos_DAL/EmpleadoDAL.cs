using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlquilerVehiculos_Entity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Sistema.DAL;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AlquilerVehiculos_DAL
{
    public class EmpleadoDAL:IRepository<Empleado>
    {
        
        private string  _conexion;
        public EmpleadoDAL(Conexion conexion)
        {
            _conexion = conexion.GetConeccion();
        }


        public bool InsertarEmpleado()
        {
            return true;
        }
        public bool Actualizar(Empleado entidad)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public List<Empleado> GetAll()
        {
            List<Empleado> empleados = new List<Empleado>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_conexion))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_GetAllEmpleados", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                empleados.Add(new Empleado
                                {
                                    Nombre = reader["Nombre"].ToString(),
                                    Apellido = reader["Apellido"].ToString(),
                                    Cargo = reader["Cargo"].ToString(),
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
                Console.WriteLine($"Error al obtener empleados: {ex.Message}");
            }

            return empleados;
        }

        public void Insertar(Empleado entidad)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertarEmpleado", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Nombre", entidad.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", entidad.Apellido);
                        cmd.Parameters.AddWithValue("@Cargo", entidad.Cargo);
                        cmd.Parameters.AddWithValue("@Telefono", entidad.Telefono);
                        cmd.Parameters.AddWithValue("@Email", entidad.Email);
                        cmd.Parameters.AddWithValue("@Clave", entidad.Clave);
                        cmd.Parameters.AddWithValue("@Restablecer_clave", entidad.Restablecer_clave);
                        cmd.Parameters.AddWithValue("@Confirmado", entidad.Confirmado);
                        cmd.Parameters.AddWithValue("@Token", entidad.Token);

                        conn.Open();
                        cmd.ExecuteNonQuery(); 
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el empleado", ex);
            }
        }

        
        public Empleado IniciarSesion(string email, string clave)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ValidarEmpleado", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Clave", clave);

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) 
                            {
                                return new Empleado
                                {
                                    Nombre = reader["Nombre"].ToString(),
                                    Restablecer_clave = Convert.ToBoolean(reader["Restablecer_clave"]),
                                    Confirmado = Convert.ToBoolean(reader["Confirmado"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar el empleado", ex);
            }

            return null; 
        }

        public Empleado GetBy(string campo, string valor)
        {
            Empleado empleado = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(_conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ObtenerEmpleadosPorCampo", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@campo",campo); // where "Email"
                        cmd.Parameters.AddWithValue("@dato", valor);    // Valor a buscar

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                empleado = new Empleado
                                {
                                    Nombre = reader["Nombre"].ToString(),
                                    Clave = reader["Clave"].ToString(),
                                    Restablecer_clave = Convert.ToBoolean(reader["Restablecer_clave"]),
                                    Confirmado = Convert.ToBoolean(reader["Confirmado"]),
                                    Token = reader["token"].ToString(),
                                    Email = reader["Email"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar el empleado", ex);
            }
            return empleado;
        }

        public bool Confirmar(string token)
        {
            bool respuesta = false;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_conexion))
                {
                    string query = "UPDATE Empleados " +
                                   "SET Confirmado = 1 " +
                                   "WHERE Token = @token";

                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@token", token);

                        sqlConnection.Open();
                        int filasAfectadas = cmd.ExecuteNonQuery();

                        respuesta =  filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al confirmar la clave del empleado", ex);
            }
            return respuesta;
        }

        public bool RestablecerActualizar(int restablece, string clave, string token)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("restablecerClave", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Restablecer_clave", restablece);
                        cmd.Parameters.AddWithValue("@Clave", clave);
                        cmd.Parameters.AddWithValue("@Token", token);

                        conn.Open();
                        int filasAfectadas = cmd.ExecuteNonQuery(); 

                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al restablecer la clave del empleado", ex);
            }
        }

        public List<Empleado> GetAllByCharacters(string variable)
        {
            throw new NotImplementedException();
        }

        
    }
}
