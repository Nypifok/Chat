using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.Models
{
    public class ChatBot
    {
        public Guid ChatId { get; set; }
        public Chat Chat { get; set; }
        public Guid BotId { get; set; }
        public Bot Bot { get; set; }
    }
}