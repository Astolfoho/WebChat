using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Api.Models;

namespace Chat.Api.Repository.EF
{
    public class MessageFileRepository : IMessageFileRepository
    {
        private DBContext _rep;

        public MessageFileRepository()
        {
            _rep = new DBContext();
        }

        public MessageFile GetById(Guid id)
        {
            return _rep.MessageFiles.FirstOrDefault(f => f.Id.Equals(id));
        }
    }
}
