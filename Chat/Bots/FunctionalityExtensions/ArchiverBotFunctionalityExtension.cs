using Chat.Data.Dtos;
using Chat.Data.Models;
using Chat.Services.Interfaces.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chat.Bots.FunctionalityExtensions
{
    public class ArchiverBotFunctionalityExtension : BotFunctionalityExtension
    {
        private readonly IServiceProvider serviceProvider;


        public ArchiverBotFunctionalityExtension(BaseBot bot, IServiceProvider serviceProvider) : base(bot)
        {
            baseBot = bot;
            this.serviceProvider = serviceProvider;
            OnMessageSended += SendLinkToArchiverQueue;
        }
        public async Task SendLinkToArchiverQueue(Message message)
        {
            var links = await GetAllLinks(message.TextContent);
            if (links.Count()>0)
            {
                using var scope = serviceProvider.CreateScope();
                var sender = scope.ServiceProvider.GetRequiredService<IQueueMessageSender>();
                var dto = new MessageWithLinksDto { ChatId = message.Chat_Id, Links = links };
               
                await sender.SendMessageToQueueAsync(JsonConvert.SerializeObject(dto), "messageLinks");
                
            }
        }
        private async Task<List<string>> GetAllLinks(string str)
        {
            var linkParser = new Regex(@"((http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var result=new List<string>();
            foreach (Match match in linkParser.Matches(str))
            {
                result.Add(match.Value);
            }
            return result;
        }
    }
}
