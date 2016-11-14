using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Api.Models;

namespace Chat.Api.Repositorios
{
    public interface IUsuarioRepositorio
    {
        IEnumerable<Models.Usuario> PegarTodos();

        IEnumerable<Models.Usuario> PegarTodosOnline();

        Models.Usuario Adicionar(Models.Usuario usuario);

        void MarcarComoOnline(int id);
        Usuario Atualizar(Usuario usuario);
        Usuario PegarPorNome(string nome);

        IEnumerable<Menssagem> PegarMenssagens(int us1, int us2);
        void AdicionarMenssagem(Menssagem msg);
    }
}
