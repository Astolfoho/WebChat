using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Api.Models
{
    public class MessageResponse:Message
    {
        public MessageResponse()
        {

        }

        public MessageResponse(Message msg, IEnumerable<MessageFile> files)
        {
            this.From = msg.From;
            this.To = msg.To;
            this.Id = msg.Id;
            this.Content = msg.Content;
            this.ItsMe = msg.ItsMe;
            this.SendDateTime = msg.SendDateTime;
            this.Files = files.Select(s => new MessageFileHeader
            {
                Content = s.Content,
                Id = s.Id,
                Extension = s.Extension,
                Name = s.Name,
                Type = s.Type
            }).ToList();    
        }


        public new IEnumerable<MessageFileHeader> Files { get; set; }

    }
}
