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
    public class MenssagensController : BaseController
    {
        private IUsuarioRepositorio _rep;

        public MenssagensController()
        {
            _rep = LocalizadorDeRepozitorios.Pegar<IUsuarioRepositorio>();
        }

        public IEnumerable<Menssagem> Get(int eu, int outro)
        {
            return _rep.PegarMenssagens(eu, outro);
        }

    }
}
