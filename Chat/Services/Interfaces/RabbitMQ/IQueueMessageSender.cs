using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Services.Interfaces.RabbitMQ
{
    public interface IQueueMessageSender
    {
        public Task SendMessageToQueueAsync(string message,string queueName);
    }
}
