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
    public class UsersController : BaseController
    {
        private IUserRepository _rep;

        public UsersController()
        {
            _rep = RepositoryLocator.Get<IUserRepository>();
        }
        public IEnumerable<User> Get()
        {
            return _rep.GetAll();
        }

        public User Post(User usuario)
        {
            User ret = null;
            if (usuario.Id != 0)
            {
                ret = _rep.Update(usuario);
            }
            else
            {
                var usr = _rep.GetByName(usuario.Name);
                if (usr != null)
                {
                    ret = _rep.Update(usuario);
                }
                else
                {
                    usuario.IsOnline = true;
                    ret = _rep.Add(usuario);
                }
            }

            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            hubContext.Clients.All.AtualizarUsuarios(_rep.GetAll());

            return ret;
        }
    }
}
