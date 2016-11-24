using Chat.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Api.Repository
{
    public interface IMessageFileRepository
    {
        MessageFile GetById(Guid id);
    }
}
