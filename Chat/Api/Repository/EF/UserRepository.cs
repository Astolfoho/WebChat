using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Api.Models;

namespace Chat.Api.Repository.EF
{
    public class UserRepository : IUserRepository
    {
        private DBContext _rep;

        public UserRepository()
        {
            _rep = new DBContext();
        }


        public User Add(User user)
        {
            user = _rep.Users.Add(user);
            _rep.SaveChanges();
            return user;
        }

        public IEnumerable<User> GetAllOnline()
        {
            return _rep.Users.Where(w => w.IsOnline);
        }

        public IEnumerable<User> GetAll()
        {
            return _rep.Users;
        }

        public User Update(User usuario)
        {
            var usr = _rep.Users.FirstOrDefault(f => f.Id == usuario.Id);
            usr.IsOnline = usuario.IsOnline;
            usr.SignalrId = usuario.SignalrId;
            _rep.SaveChanges();
            return usuario;
        }

        public User GetByName(string name)
        {
            return _rep.Users.FirstOrDefault(f => f.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        public IEnumerable<MessageResponse> GetMessages(int us1, int us2)
        {
            var ret = (from m in this._rep.Messages
                       where (m.To == us1 && m.From == us2)
                            || (m.To == us2 && m.From == us1)
                       orderby m.SendDateTime descending
                       select m).ToList() ;



            return ret.Select(s => {
                var r = new MessageResponse(s, (from f in _rep.MessageFiles
                                                where f.MessageId == s.Id
                                                select f));
                r.NameFrom = this._rep.Users.First(f2 => f2.Id == s.From).Name;
                r.ItsMe = s.From == us1;
                return r;
            }
            );
        }

        public void AddMessage(Message msg)
        {
            _rep.Messages.Add(msg);
            _rep.SaveChanges();
            foreach (var file in msg.Files)
            {
                file.MessageId = msg.Id;
                _rep.MessageFiles.Add(file);
            }
            _rep.SaveChanges();


        }

    }
}
