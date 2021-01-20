using Chat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Bots
{
    public abstract class BaseBot
    {
        public Guid BotId { get; set; }
        public BaseBot(Guid botId)
        {
            BotId = botId;
        }
        public abstract Task HandleMessage(Message message,IServiceProvider serviceProvider);
    }
}
