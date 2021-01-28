using Chat.Bots;
using Chat.Data.Models;
using Chat.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Services.Implementations
{
    public class BotNotificator : IBotNotificator
    {

        public event Func<Message,Task> OnMessageSended;
        public BotNotificator()
        {
        }
        public async Task Notificate(Message message)
        {
            OnMessageSended?.Invoke(message).Start();
        }
    }
}
