using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.Api.Models
{
    [Table("MessageFiles")]
    public class MessageFile : MessageFileHeader
    {
        public byte[] Bytes { get; set; }
    }
}