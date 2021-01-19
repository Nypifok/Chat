using Chat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Bots
{
    public abstract class BaseBot
    {
        public Message Message { get; protected set; }
        protected IDataContext context { get; set; }
        public BaseBot(Message message,IDataContext context)
        {
            this.context = context;
            if (Message == null) throw new ArgumentNullException(nameof(message));

            Message = message;
        }
    }
}
