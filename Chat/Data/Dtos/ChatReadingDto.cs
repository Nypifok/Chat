using Chat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.Dtos
{
    public class ChatReadingDto
    {
        public Message LastReadedMessage { get; set; }
        public Guid ChatId { get; set; }
    }
}
