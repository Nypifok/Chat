using Chat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Bots
{
    public abstract class BotFunctionalityExtension : BaseBot
    {
        protected BaseBot baseBot { get; set; }
        public BotFunctionalityExtension(Message message, IDataContext context,BaseBot bot) : base(message, context)
        {
            baseBot = bot ?? throw new ArgumentNullException(nameof(bot));
            Message = bot.Message;
            this.context = context;
        }

    }
}
