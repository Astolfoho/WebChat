using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Api.Models;

namespace Chat.Api.Repositorios.EF
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private DBContext _rep;

        public UsuarioRepositorio()
        {
            _rep = new DBContext();
        }


        public Usuario Adicionar(Usuario usuario)
        {
            usuario = _rep.Usuarios.Add(usuario);
            _rep.SaveChanges();
            return usuario;
        }

        public IEnumerable<Usuario> PegarTodosOnline()
        {
            return _rep.Usuarios.Where(w => w.EstaOnline);
        }

        public IEnumerable<Usuario> PegarTodos()
        {
            return _rep.Usuarios;
        }

        public Usuario Atualizar(Usuario usuario)
        {
            var usr = _rep.Usuarios.FirstOrDefault(f => f.Id == usuario.Id);
            usr.EstaOnline = usuario.EstaOnline;
            usr.SignalrId = usuario.SignalrId;
            _rep.SaveChanges();
            return usuario;
        }

        public void MarcarComoOnline(int id)
        {
            throw new NotImplementedException();
        }

        public Usuario PegarPorNome(string nome)
        {
            return _rep.Usuarios.FirstOrDefault(f => f.Nome.Equals(nome, StringComparison.InvariantCultureIgnoreCase));
        }

        public IEnumerable<Menssagem> PegarMenssagens(int us1, int us2)
        {
            var ms1 = this._rep.Menssagens.Where(w => w.Para == us1 && w.De == us2 || w.Para == us2 && w.De == us1).OrderBy(o => o.SendDateTime);
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
            _rep.Menssagens.Add(msg);
            _rep.SaveChanges();
        }
    }
}
