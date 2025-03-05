using System;
using System.Collections.Generic;
using System.Data;
using AlquilerVehiculos_Entity;
using Azure;
using Microsoft.Data.SqlClient;
using Sistema.DAL;

namespace AlquilerVehiculos_DAL
{
    public class PagoDAL : IRepository<Pago>
    {
        private readonly string connectionString;

        public PagoDAL(Conexion conexion)
        {
            connectionString = conexion.GetConeccion();
        }

        // Obtener todos los pagos
        public List<Pago> GetAll()
        {
            List<Pago> pagos = new List<Pago>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerPagos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pagos.Add(new Pago
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        ReservaId = Convert.ToInt32(reader["ReservaId"]),
                        Monto = Convert.ToDecimal(reader["Monto"]),
                        MetodoPago = reader["MetodoPago"].ToString(),
                        FechaPago = Convert.ToDateTime(reader["FechaPago"])
                    });
                }
            }
            return pagos;
        }

        // Obtener un pago por ID
        public Pago GetBy(string campo, string valor)
        {
            Pago pago = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM Pagos WHERE {campo} = @Valor";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Valor", valor);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    pago = new Pago
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        ReservaId = Convert.ToInt32(reader["ReservaId"]),
                        Monto = Convert.ToDecimal(reader["Monto"]),
                        MetodoPago = reader["MetodoPago"].ToString(),
                        FechaPago = Convert.ToDateTime(reader["FechaPago"])
                    };
                }
            }

            return pago;
        }

        // Insertar un pago
        public void Insertar(Pago pago)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarPago", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReservaId", pago.ReservaId);
                cmd.Parameters.AddWithValue("@Monto", pago.Monto);
                cmd.Parameters.AddWithValue("@MetodoPago", pago.MetodoPago);
                cmd.Parameters.AddWithValue("@FechaPago", pago.FechaPago);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Actualizar un pago
        public bool Actualizar(Pago pago)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarPago", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", pago.Id);
                cmd.Parameters.AddWithValue("@ReservaId", pago.ReservaId);
                cmd.Parameters.AddWithValue("@Monto", pago.Monto);
                cmd.Parameters.AddWithValue("@MetodoPago", pago.MetodoPago);
                cmd.Parameters.AddWithValue("@FechaPago", pago.FechaPago);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Eliminar un pago por ID
        public bool Eliminar(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarPago", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        // Método no implementado
        public List<Pago> GetAllByCharacters(string variable)
        {
            throw new NotImplementedException();
        }
    }
}
