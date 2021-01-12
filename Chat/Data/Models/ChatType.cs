using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.Models
{
    public class ChatType
    {
        public string Type { get; set; }
        public IEnumerable<Chat> Chats { get; set; }
        public ChatType()
        {
            Chats = new List<Chat>();
        }
    }
}
