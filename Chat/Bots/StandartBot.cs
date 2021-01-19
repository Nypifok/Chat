using Chat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Bots
{
    public class StandartBot : BaseBot
    {
        public StandartBot(Message message, IDataContext context) : base(message, context)
        {
        }
    }
}
