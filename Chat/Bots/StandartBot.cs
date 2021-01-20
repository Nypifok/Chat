using Chat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Bots
{
    public class StandartBot : BaseBot
    {
        public StandartBot(Guid botId) : base(botId)
        {
        }

        public override Task HandleMessage(Message message,IServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }
    }
}
