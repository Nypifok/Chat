using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.Dtos
{
    public class MessageEditingDto
    {
        public Guid MessageId { get; set; }
        public string MessageContent { get; set; }
    }
}
