using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.Dtos
{
    public class MessageSendingDto
    {
        public Guid ChatId { get; set; }
        public string MessageContent { get; set; }
    }
}
