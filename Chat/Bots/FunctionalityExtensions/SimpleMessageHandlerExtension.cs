using Chat.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Bots.FunctionalityExtensions
{
    public class SimpleMessageHandlerExtension : BotFunctionalityExtension
    {
        public SimpleMessageHandlerExtension(BaseBot bot) : base(bot)
        {
            baseBot = bot;
        }
        public override Task HandleMessage(Message message,IServiceProvider serviceProvider)
        {
            using var scope=serviceProvider.CreateScope();
            var context=scope.ServiceProvider.GetRequiredService<IDataContext>();
            context.BeginTransaction();
            context.Messages.Add(new Message {SendingTime=DateTime.Now,BotId=BotId,Chat_Id=message.Chat_Id,TextContent="Привет!" });
            context.Commit();
            return base.HandleMessage(message,serviceProvider);
        }
    }
}
