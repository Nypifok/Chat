using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid Chat_Id { get; set; }
        public Chat Chat { get; set; }
        public Guid? AuthorId { get; set; }
        public User Author { get; set; }
        public Guid? BotId { get; set; }
        public Bot Bot { get; set; }
        public string TextContent { get; set; }
        public DateTime SendingTime { get; set; }
        public bool IsEdited { get; set; }
        public bool IsReaded { get; set; }
        public bool IsSystemMessage { get; set; }
    }
}
