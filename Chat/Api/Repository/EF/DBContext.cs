using Chat.Api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Api.Repository.EF
{
    public class DBContext : DbContext
    {
        public DBContext()
            :base("Default")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DbSet<MessageFile> MessageFiles { get; set; }
    }
}
