using Chat.Api.Hubs;
using Chat.Api.Models;
using Chat.Api.Repositorios;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Api
{
    public class UsuariosController : BaseController
    {
        private IUsuarioRepositorio _rep;

        public UsuariosController()
        {
            _rep = LocalizadorDeRepozitorios.Pegar<IUsuarioRepositorio>();
        }
        public IEnumerable<Usuario> Get()
        {
            return _rep.PegarTodos();
        }

        public Usuario Post(Usuario usuario)
        {
            Usuario ret = null;
            if (usuario.Id != 0)
            {
                ret = _rep.Atualizar(usuario);
            }
            else
            {
                var usr = _rep.PegarPorNome(usuario.Nome);
                if (usr != null)
                {
                    ret = _rep.Atualizar(usuario);
                }
                else
                {
                    usuario.EstaOnline = true;
                    ret = _rep.Adicionar(usuario);
                }
            }

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            hubContext.Clients.All.AtualizarUsuarios(_rep.PegarTodos());

            return ret;
        }
    }
}
