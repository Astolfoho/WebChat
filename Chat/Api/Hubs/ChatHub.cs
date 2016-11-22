using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Chat.Api.Models;
using Chat.Api.Repository;

namespace Chat.Api.Hubs
{
    public class ChatHub : Hub
    {
        private IUserRepository _usersRep;

        public ChatHub()
        {
            _usersRep = RepositoryLocator.Get<IUserRepository>();
        }

        public void SendMessage(Message msg)
        {
            var userTo = this._usersRep.GetAll().FirstOrDefault(w => w.Id == msg.To);
            msg.SendDateTime = DateTime.Now;
            _usersRep.AddMessage(msg);
            this.Clients.Client(userTo.SignalrId).OnMessageReceived(msg);
        }

        public IEnumerable<User> GetUsers()
        {
            
            return _usersRep.GetAll();
        }
        
    }
}