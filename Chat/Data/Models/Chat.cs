using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.Models
{
    public class Chat
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public ChatType ChatType { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public IEnumerable<ChatMember> ChatMembers { get; set; }
        public Chat()
        {
            ChatMembers = new List<ChatMember>();
            Messages = new List<Message>();
        }
    }
}
