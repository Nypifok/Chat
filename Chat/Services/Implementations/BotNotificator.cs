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
        private readonly IDataContext context;
        public BotNotificator(IDataContext context)
        {
            this.context = context;
        }
        public async Task Notificate(Message message)
        {
            context.ChatBots.Where(cb => cb.ChatId == message.Chat_Id);
        }
    }
}
