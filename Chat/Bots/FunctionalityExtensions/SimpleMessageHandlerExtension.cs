using Chat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Bots.FunctionalityExtensions
{
    public class SimpleMessageHandlerExtension : BotFunctionalityExtension
    {
        public SimpleMessageHandlerExtension(Message message, IDataContext context, BaseBot bot) : base(message, context, bot)
        {
            if (message.TextContent.Contains("time"))
            {
               
            }
                
        }
    }
}
