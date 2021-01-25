using Chat.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Bots.FunctionalityExtensions
{
    public class HiBotFunctionalityExtension : BotFunctionalityExtension
    {
        private readonly IServiceProvider serviceProvider;
        public HiBotFunctionalityExtension(BaseBot bot,IServiceProvider serviceProvider) : base(bot)
        {
            baseBot = bot;
            this.serviceProvider = serviceProvider;
            OnEventHappened +=SendHiMessage;
        }
        public async Task SendHiMessage(Message message)
        {
            using var scope=serviceProvider.CreateScope();
            var context=scope.ServiceProvider.GetRequiredService<IDataContext>();
            await context.BeginTransaction();
            context.Messages.Add(new Message {SendingTime=DateTime.Now,BotId=BotId,Chat_Id=message.Chat_Id,TextContent=$"Hi!" });
            await context.Commit();
        }
    }
}
