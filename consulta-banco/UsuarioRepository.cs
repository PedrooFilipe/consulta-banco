using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace consulta_banco
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public Contexto _contexto;

        public UsuarioRepository(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task SaveAll()
        {
            List <Usuario> usuarios =  new List<Usuario>
            {
                new Usuario{ Nome = "Test", Email= "asd@gmail.com"},
                new Usuario{ Nome = "Test", Email= "asd@gmail.com"},
                new Usuario{ Nome = "Test", Email= "asd@gmail.com"},
                new Usuario{ Nome = "Test", Email= "asd@gmail.com"},
                new Usuario{ Nome = "Test", Email= "asd@gmail.com"}
            };

            await _contexto.Usuarios.AddRangeAsync(usuarios);
            await _contexto.SaveChangesAsync();
        }

        public async Task UpdateAll()
        {
            List<Usuario> usuariosAntigos = _contexto.Usuarios.Where(u => u.Email.Equals("asd@gmail.com")).ToList();

            foreach(Usuario usuario in usuariosAntigos)
            {
                usuario.Email = "novo@gmail.com";

                _contexto.Entry(usuario).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
            }
        }


    }
}
