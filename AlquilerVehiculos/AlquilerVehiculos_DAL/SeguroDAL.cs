using System;
using System.Collections.Generic;
using System.Data;
using AlquilerVehiculos_Entity;
using Microsoft.Data.SqlClient;
using Sistema.DAL;

namespace AlquilerVehiculos_DAL
{
    public class SeguroDAL : IRepository<Seguro>
    {
        private readonly string connectionString;

        public SeguroDAL(Conexion conexion)
        {
            connectionString = conexion.GetConeccion();
        }

        // Obtener todos los seguros con información del cliente y reserva
        public List<Seguro> GetAll()
        {
            List<Seguro> seguros = new List<Seguro>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerSeguros", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    seguros.Add(new Seguro
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        ReservaId = Convert.ToInt32(reader["ReservaId"]),
                        ClienteNombre = reader["ClienteNombre"].ToString(),
                        TipoSeguro = reader["TipoSeguro"].ToString(),
                        Costo = Convert.ToDecimal(reader["Costo"])
                    });
                }
            }
            return seguros;
        }

        // Obtener un seguro por ID
        public Seguro GetBy(string campo, string valor)
        {
            Seguro seguro = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM Seguros WHERE {campo} = @Valor";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Valor", valor);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    seguro = new Seguro
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        ReservaId = Convert.ToInt32(reader["ReservaId"]),
                        TipoSeguro = reader["TipoSeguro"].ToString(),
                        Costo = Convert.ToDecimal(reader["Costo"])
                    };
                }
            }

            return seguro;
        }

        // Insertar un nuevo seguro
        public void Insertar(Seguro seguro)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarSeguro", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReservaId", seguro.ReservaId);
                cmd.Parameters.AddWithValue("@TipoSeguro", seguro.TipoSeguro);
                cmd.Parameters.AddWithValue("@Costo", seguro.Costo);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Actualizar un seguro existente
        public bool Actualizar(Seguro seguro)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarSeguro", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", seguro.Id);
                cmd.Parameters.AddWithValue("@ReservaId", seguro.ReservaId);
                cmd.Parameters.AddWithValue("@TipoSeguro", seguro.TipoSeguro);
                cmd.Parameters.AddWithValue("@Costo", seguro.Costo);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Eliminar un seguro por ID
        public bool Eliminar(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarSeguro", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public List<Reserva> ObtenerReservasSinSeguro()
        {
            List<Reserva> reservas = new List<Reserva>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerReservasSinSeguro", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    reservas.Add(new Reserva
                    {
                        Id = Convert.ToInt32(reader["ReservaId"]),
                        ClienteNombre = reader["ClienteNombre"].ToString(),
                        VehiculoNombre = reader["VehiculoNombre"].ToString(),
                        FechaInicio = Convert.ToDateTime(reader["FechaInicio"]),
                        FechaFin = Convert.ToDateTime(reader["FechaFin"]),
                        Estado = reader["Estado"].ToString() // ✅ Agregamos Estado para evitar el error
                    });


                }
            }
            return reservas;
        }

        // Método no implementado
        public List<Seguro> GetAllByCharacters(string variable)
        {
            throw new NotImplementedException();
        }
    }
}
