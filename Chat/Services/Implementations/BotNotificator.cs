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
        private readonly IBotBuilder builder;
        public BotNotificator(IBotBuilder builder)
        {
            this.builder = builder;
        }
        public async Task Notificate(Message message)
        {
            builder.BuildBots(message);
        }
    }
}
