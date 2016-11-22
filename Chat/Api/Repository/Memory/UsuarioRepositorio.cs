using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Api.Models;

namespace Chat.Api.Repository.Memory
{
    public class UsuarioRepositorio : IUserRepository
    {
        private static List<User> _rep;
        private static List<Message> _repMsg;

        private static object _sync = new object();

        static UsuarioRepositorio()
        {
            _rep = new List<User>();
            _repMsg = new List<Message>();
        }


        public User Add(User usr)
        {
            lock (_sync)
            {
                _rep.Add(usr);
                usr.Id = _rep.Max(m => m.Id) + 1;
                return usr;
            }
        }


        public IEnumerable<User> GetAllOnline()
        {
            if (!_rep.Any())
            {
                return new List<User>();
            }
            return _rep.Where(w => w.IsOnline);
        }

        public IEnumerable<User> GetAll()
        {
            return _rep;
        }

        public User Update(User user)
        {
            var usr = _rep.FirstOrDefault(f => f.Id == user.Id);

            if(usr == null)
            {
                this.Add(user);
                return user;
            }
            usr.IsOnline = user.IsOnline;
            usr.SignalrId = user.SignalrId;
            return user;
        }

        public User GetByName(string name)
        {
            return _rep.FirstOrDefault(f => f.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        public IEnumerable<Message> GetMessages(int us1, int us2)
        {
            var ms1 = _repMsg.Where(w => w.To == us1 && w.From == us2 || w.To == us2 && w.From == us1).OrderByDescending(o => o.SendDateTime);
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
            _repMsg.Add(msg);
        }

    }
}
