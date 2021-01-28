using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.Dtos
{
    public class MessageWithLinksDto
    {
        public Guid ChatId { get; set; }
        public List<string> Links { get; set; }
        public MessageWithLinksDto()
        {
        }
    }
}
