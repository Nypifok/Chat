using Chat.Data;
using Chat.Services.Interfaces.RabbitMQ;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Services.Implementations.RabbitMQ
{
    public class QueueMessageSender : IQueueMessageSender
    {
        private readonly string hostname;
        private readonly string password;
        private readonly string username;
        private IConnection connection;
        public QueueMessageSender(IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            this.hostname = rabbitMqOptions.Value.Hostname;
            this.username = rabbitMqOptions.Value.UserName;
            this.password = rabbitMqOptions.Value.Password;
        }
        public async Task SendMessageToQueueAsync(string message, string queueName)
        {
            if (ConnectionExists())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
                }
            }
        }
        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = hostname,
                    UserName = username,
                    Password = password
                };
                connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
            }
        }

        private bool ConnectionExists()
        {
            if (connection != null)
            {
                return true;
            }

            CreateConnection();

            return connection != null;
        }
    }
}

