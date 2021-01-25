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
        public event Func<Message,Task> OnMessageSended;
        public event Func<Message, Task> OnEventHappened;
        public BaseBot(Guid botId) 
        {
            BotId = botId;

        }
        public async Task InvokeOnEventHappened(Message message)
        {
            OnEventHappened?.Invoke(message);
        }
        public async Task InvokeOnMessageSended(Message message)
        {
            OnMessageSended?.Invoke(message);
        }
    }
}
