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
        public BotFunctionalityExtension(BaseBot bot) : base(bot.BotId)
        {
            baseBot = bot ?? throw new ArgumentNullException(nameof(bot));
            BotId = bot.BotId;
        }
    }
}
