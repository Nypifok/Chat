﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.Models
{
    public class Bot
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<BotType> BotTypes { get; set; }
        public IEnumerable<ChatBot> ChatBots { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public Bot()
        {
            ChatBots = new List<ChatBot>();
            BotTypes = new List<BotType>();
            Messages = new List<Message>();
        }
    }
}
