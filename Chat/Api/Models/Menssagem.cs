using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Api.Models
{
    public class Menssagem
    {
        public Guid Id { get; set; }
        public int De { get; set; }
        public int Para { get; set; }
        public String Conteudo { get; set; }
        public DateTime SendDateTime { get; set; }

        [NotMapped]
        public string NomeUsuarioDe { get; set; }

        [NotMapped]
        public bool SouEu { get; set; }
    }
}
