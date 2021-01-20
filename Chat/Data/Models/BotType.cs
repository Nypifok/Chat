using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.Models
{
    public class BotType
    {
        public Guid Id { get; set; }
        public Guid BotId { get; set; }
        public Bot Bot { get; set; }
        public string TypeId { get; set; }
        public TypeOfBot Type { get; set; }
    }
}