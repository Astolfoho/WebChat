using Chat.Api.Hubs;
using Chat.Api.Models;
using Chat.Api.Repository;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Api
{
    public class MessagesController : BaseController
    {
        private IUserRepository _rep;

        public MessagesController()
        {
            _rep = RepositoryLocator.Get<IUserRepository>();
        }

        public IEnumerable<Message> Get(int eu, int outro)
        {
            return _rep.GetMessages(eu, outro);
        }

    }
}
