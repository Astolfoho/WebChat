using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Api.Models;

namespace Chat.Api.Repositorios.Memoria
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private static List<Usuario> _rep;

        private static object _sync = new object();

        static UsuarioRepositorio()
        {
            _rep = new List<Usuario>
            {
                new Usuario
                {
                    Id = 1,
                    Nome = "teste",
                    EstaOnline = false
                }
            };
        }


        public Usuario Adicionar(Usuario usuario)
        {
            lock (_sync)
            {
                _rep.Add(usuario);
                usuario.Id = _rep.Max(m => m.Id) + 1;
                return usuario;
            }
        }

        public void MarcarComoOnline(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> PegarTodosOnline()
        {
            if (!_rep.Any())
            {
                return new List<Usuario>();
            }
            return _rep.Where(w => w.EstaOnline);
        }

        public IEnumerable<Usuario> PegarTodos()
        {
            return _rep;
        }

        public Usuario Atualizar(Usuario usuario)
        {
            var usr = _rep.FirstOrDefault(f => f.Id == usuario.Id);

            if(usr == null)
            {
                this.Adicionar(usuario);
                return usuario;
            }
            usr.EstaOnline = usuario.EstaOnline;
            return usuario;
        }

        public Usuario PegarPorNome(string nome)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Menssagem> PegarMenssagens(int us1, int us2)
        {
            throw new NotImplementedException();
        }

        public void AdicionarMenssagem(Menssagem msg)
        {
            throw new NotImplementedException();
        }
    }
}
