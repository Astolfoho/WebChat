using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Api.Models
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public String Content { get; set; }
        public DateTime SendDateTime { get; set; }

        [NotMapped]
        public string NameFrom { get; set; }

        [NotMapped]
        public bool ItsMe { get; set; }
        public IEnumerable<MessageFile> Files { get; set; }
    }
}
