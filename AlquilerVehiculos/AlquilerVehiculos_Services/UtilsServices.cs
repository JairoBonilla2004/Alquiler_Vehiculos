using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AlquilerVehiculos_Services
{
    public class UtilsServices
    {
        public static string ConvertirASHA256(string texto)
        {
            using (SHA256 sha256 = SHA256.Create()) 
            {

                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texto));

                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                {
                    sb.Append(b.ToString("x2")); 
                }

                return sb.ToString();
            }
        }

        public static string GenerarToken()
        {
            string token = Guid.NewGuid().ToString("N");
            return token;
        }
    }
}
