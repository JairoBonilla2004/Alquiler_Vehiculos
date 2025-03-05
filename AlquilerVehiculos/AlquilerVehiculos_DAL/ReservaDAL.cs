using System;
using System.Collections.Generic;
using System.Data;
using AlquilerVehiculos_Entity;
using Microsoft.Data.SqlClient;
using Sistema.DAL;

namespace AlquilerVehiculos_DAL
{
    public class ReservaDAL : IRepository<Reserva>
    {
        private readonly string connectionString;

        public ReservaDAL(Conexion conexion)
        {
            connectionString = conexion.GetConeccion();
        }

        // Obtener todas las reservas con Cliente y Vehículo
        public List<ReservaDetalle> GetAll()
        {
            List<ReservaDetalle> reservas = new List<ReservaDetalle>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerReservasDetalladas", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    reservas.Add(new ReservaDetalle
                    {
                        Id = Convert.ToInt32(reader["ReservaId"]),
                        ClienteNombre = reader["ClienteNombre"].ToString(),
                        VehiculoNombre = reader["VehiculoNombre"].ToString(),
                        FechaInicio = Convert.ToDateTime(reader["FechaInicio"]),
                        FechaFin = Convert.ToDateTime(reader["FechaFin"]),
                        Estado = reader["Estado"].ToString()
                    });
                }
            }
            return reservas;
        }

        // Obtener una reserva por ID con detalles
        public ReservaDetalle GetBy(string campo, string valor)
        {
            ReservaDetalle reserva = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerReservasDetalladas", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Campo", campo);
                    cmd.Parameters.AddWithValue("@Valor", valor);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            reserva = new ReservaDetalle
                            {
                                Id = Convert.ToInt32(reader["ReservaId"]),
                                ClienteNombre = reader["ClienteNombre"].ToString(),
                                VehiculoNombre = reader["VehiculoNombre"].ToString(),
                                FechaInicio = Convert.ToDateTime(reader["FechaInicio"]),
                                FechaFin = Convert.ToDateTime(reader["FechaFin"]),
                                Estado = reader["Estado"].ToString()
                            };
                        }
                    }
                }
            }
            return reserva;
        }


        // Insertar una nueva reserva
        public void Insertar(Reserva reserva)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarReserva", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ClienteId", reserva.ClienteId);
                cmd.Parameters.AddWithValue("@VehiculoId", reserva.VehiculoId);
                cmd.Parameters.AddWithValue("@FechaInicio", reserva.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", reserva.FechaFin);
                cmd.Parameters.AddWithValue("@Estado", reserva.Estado);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Actualizar una reserva existente
        public bool Actualizar(Reserva reserva)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarReserva", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", reserva.Id);
                cmd.Parameters.AddWithValue("@ClienteId", reserva.ClienteId);
                cmd.Parameters.AddWithValue("@VehiculoId", reserva.VehiculoId);
                cmd.Parameters.AddWithValue("@FechaInicio", reserva.FechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", reserva.FechaFin);
                cmd.Parameters.AddWithValue("@Estado", reserva.Estado);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Eliminar una reserva por ID
        public bool Eliminar(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarReserva", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Método que no se usará en esta implementación
        public List<Reserva> GetAllByCharacters(string variable)
        {
            throw new NotImplementedException();
        }

       

        Reserva IRepository<Reserva>.GetBy(string campo, string valor)
        {
            throw new NotImplementedException();
        }

        List<Reserva> IRepository<Reserva>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}