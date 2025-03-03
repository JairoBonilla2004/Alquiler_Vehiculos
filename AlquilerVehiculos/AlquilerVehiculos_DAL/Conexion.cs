using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Sistema.DAL
{
    public class Conexion
    {
        private readonly string _conexion;

        public Conexion(IConfiguration configuration)
        {
            _conexion = configuration.GetConnectionString("SQLServerConnection");
        }

        public string GetConeccion()
        {
            return _conexion;
        }
    }
}
