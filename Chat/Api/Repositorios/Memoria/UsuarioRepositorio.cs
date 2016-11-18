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
        private static List<Menssagem> _repMsg;

        private static object _sync = new object();

        static UsuarioRepositorio()
        {
            _rep = new List<Usuario>();
            _repMsg = new List<Menssagem>();
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
            usr.SignalrId = usuario.SignalrId;
            return usuario;
        }

        public Usuario PegarPorNome(string nome)
        {
            return _rep.FirstOrDefault(f => f.Nome.Equals(nome, StringComparison.InvariantCultureIgnoreCase));
        }

        public IEnumerable<Menssagem> PegarMenssagens(int us1, int us2)
        {
            var ms1 = _repMsg.Where(w => w.Para == us1 && w.De == us2 || w.Para == us2 && w.De == us1).OrderByDescending(o => o.SendDateTime);
            var usrs = this.PegarTodos();
            ms1.ToList().ForEach(f =>
            {
                f.NomeUsuarioDe = usrs.First(f2 => f2.Id == f.De).Nome;
                f.SouEu = f.De == us1;
            });
            return ms1;
        }

        public void AdicionarMenssagem(Menssagem msg)
        {
            _repMsg.Add(msg);
        }
    }
}
