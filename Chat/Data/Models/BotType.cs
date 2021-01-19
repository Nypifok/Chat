using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.Models
{
    public class BotType
    {
        public Guid BotId { get; set; }
        public Bot Bot { get; set; }
        public Guid TypeId { get; set; }
        public TypeOfBot Type { get; set; }
    }
}