using Chat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Bots
{
    public class BotBuilder : IBotBuilder
    {
        private readonly IDataContext context;
        public BotBuilder(IDataContext context)
        {
            this.context = context;
        }
        public void BuildBots(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
