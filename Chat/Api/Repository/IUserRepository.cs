using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Api.Models;

namespace Chat.Api.Repository
{
    public interface IUserRepository
    {
        IEnumerable<Models.User> GetAll();

        IEnumerable<Models.User> GetAllOnline();

        Models.User Add(Models.User user);

        User Update(User user);

         Models.User GetByName(string name);


        //todo Create Other repository for this
        IEnumerable<MessageResponse> GetMessages(int us1, int us2);
        void AddMessage(Message msg);
    }
}
