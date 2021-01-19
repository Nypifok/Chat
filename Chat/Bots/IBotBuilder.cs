using Chat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Bots
{
    public interface IBotBuilder
    {
        void BuildBots(Message message);
    }
}
