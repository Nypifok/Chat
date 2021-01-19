using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.Models
{
    public  class TypeOfBot
    {
        public string Title { get; set; }
        public IEnumerable<BotType> BotTypes { get; set; }
        public TypeOfBot()
        {
            BotTypes = new List<BotType>();
        }
    }
}