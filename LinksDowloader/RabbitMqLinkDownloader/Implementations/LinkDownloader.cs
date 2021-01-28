using Chat.Data;
using Chat.Data.Dtos;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinksDowloader.RabbitMqLinkDownloader.Implementations
{
    public class LinkDownloader : BackgroundService
    {
        private IConnection connection;
        private IModel channel;
        private readonly string hostname;
        private readonly string queueName = "messageLinks";
        private readonly string username;
        private readonly string password;
        private readonly string pathToSave = @"C:\Users\Nypifok\Desktop\TestDownloads";
        public LinkDownloader(IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            this.hostname = rabbitMqOptions.Value.Hostname;
            this.username = rabbitMqOptions.Value.UserName;
            this.password = rabbitMqOptions.Value.Password;
            InitializeRabbitMqListener();
        }
        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = hostname,
                UserName = username,
                Password = password
            };

            connection = factory.CreateConnection();
            connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var links = JsonConvert.DeserializeObject<MessageWithLinksDto>(content);

                HandleMessage(links);

                channel.BasicAck(ea.DeliveryTag, false);
            };
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerCancelled;

            channel.BasicConsume(queueName, false, consumer);

            return Task.CompletedTask;
        }
        private async Task HandleMessage(MessageWithLinksDto dto)
        {
            foreach (string str in dto.Links)
            {
                try
                {
                    WebRequest request = WebRequest.Create(new Uri(str));
                    request.Method = "HEAD";

                    using (WebResponse response = request.GetResponse())
                    {
                        if (response.ContentLength != 0 && !string.IsNullOrWhiteSpace(response.ContentType))
                        {
                            using var client = new WebClient();
                            CreateDownload(str, pathToSave+"\\"+dto.ChatId.ToString(), response.ContentLength);
                        }
                    }
                }
                catch
                {
                    continue;
                }


            }
        }
        private async Task CreateDownload(string url, string pathToSave, long fileSize)
        {
            Directory.CreateDirectory(pathToSave);
            var finalPathToSave = pathToSave + "\\" + Path.GetFileName(url);
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    using (
                        Stream contentStream = await (await httpClient.SendAsync(request)).Content.ReadAsStreamAsync(),
                        stream = new FileStream(finalPathToSave, FileMode.Create))
                    {
                        var buffer = new byte[4096];
                        while (true)
                        {
                            var length = contentStream.Read(buffer, 0, buffer.Length);
                            if (length <= 0)
                            {
                                break;
                            }

                            await stream.WriteAsync(buffer, 0, length);
                        }
                    }
                }
            }

        }

        private void OnConsumerCancelled(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
        }

    }
}
