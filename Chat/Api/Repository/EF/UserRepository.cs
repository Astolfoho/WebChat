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

        public IEnumerable<Message> GetMessages(int us1, int us2)
        {
            var ms1 = this._rep.Messages.Where(w => w.To == us1 && w.From == us2 || w.To == us2 && w.From == us1).OrderByDescending(o => o.SendDateTime);
            var usrs = this.GetAll();
            ms1.ToList().ForEach(f =>
                {
                    f.NameFrom = usrs.First(f2 => f2.Id == f.From).Name;
                    f.ItsMe = f.From == us1;
                });
            return ms1;
        }

        public void AddMessage(Message msg)
        {
            msg.Id = Guid.NewGuid();
            _rep.Messages.Add(msg);
            _rep.SaveChanges();
        }

    }
}
