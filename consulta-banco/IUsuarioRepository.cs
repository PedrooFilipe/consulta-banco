using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consulta_banco
{
    public interface IUsuarioRepository
    {
        Task SaveAll();
        Task UpdateAll();
    }
}
