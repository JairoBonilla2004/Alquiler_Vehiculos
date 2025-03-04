using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.DAL
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T GetBy(string campo, string valor);
        List<T> GetAllByCharacters(string variable);
        void Insertar(T entidad);
        bool Actualizar(T entidad);
        bool Eliminar(int id);
    }
}
