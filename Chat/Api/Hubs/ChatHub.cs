using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Chat.Api.Models;
using Chat.Api.Repositorios;

namespace Chat.Api.Hubs
{
    public class ChatHub : Hub
    {
        private IUsuarioRepositorio _usersRep;

        public ChatHub()
        {
            _usersRep = LocalizadorDeRepozitorios.Pegar<IUsuarioRepositorio>();
        }

        public void SendMessage(Menssagem msg)
        {
            var userPara = this._usersRep.PegarTodos().FirstOrDefault(w => w.Id == msg.Para);
            msg.SendDateTime = DateTime.Now;
            _usersRep.AdicionarMenssagem(msg);
            this.Clients.Client(userPara.SignalrId).MenssagemRecebida(msg);
        }

        public IEnumerable<Usuario> PegarUsuarios()
        {
            
            return _usersRep.PegarTodos();
        }
        
    }
}