using Chat.Api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Api.Repositorios.EF
{
    public class DBContext : DbContext
    {
        public DBContext()
            :base("Default")
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Menssagem> Menssagens { get; set; }
    }
}
