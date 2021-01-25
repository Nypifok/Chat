using Chat.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Chat.Bots.FunctionalityExtensions
{
    public class DadJokeBotFunctionalityExtension : BotFunctionalityExtension
    {
        private readonly IServiceProvider serviceProvider;
        private readonly List<string> BoryWords = new List<string> { "bory","boring","bored","tedious","dull","monotonous",
                                                "repetitious","repetitive","unrelieved","lacking variety","lacking variation","lacking excitement","lacking interest","unvaried","unimaginative",
                                                "uneventful","characterless","featureless","colourless","lifeless","soulless","passionless","spiritless","unspirited","insipid","uninteresting","unexciting",
                                                "uninspiring","unstimulating","unoriginal","derivative","jejune","nondescript","sterile","flat","bland","arid","dry","dry as dust","stale","wishy-washy",
                                                "grey","anaemic","tired","banal","lame","plodding","ponderous","pedestrian","lacklustre","stodgy","dreary","mechanical","stiff","leaden","wooden",
                                                "mind-numbing","soul-destroying","wearisome","tiring","tiresome","irksome","trying","frustrating","humdrum","prosaic","mundane","commonplace","workaday",
                                                "quotidian","unremarkable","routine","run-of-the-mill","normal","usual","ordinary","conventional","suburban","garden variety","deadly",
                                                "bog-standard","nothing to write home about","a dime a dozen","no great shakes","not up to much","samey","common or garden","dullsville","ornery"
                                                };
        public DadJokeBotFunctionalityExtension(BaseBot bot, IServiceProvider serviceProvider) : base(bot)
        {
            baseBot = bot;
            this.serviceProvider = serviceProvider;
            OnMessageSended += SendJoke;
        }
        public async Task SendJoke(Message message)
        {
            if (ContainsAnyBoryWords(message.TextContent))
            {
                using var scope = serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<IDataContext>();
                var httpFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
                await context.BeginTransaction();
                context.Messages.Add(new Message { SendingTime = DateTime.Now, BotId = BotId, Chat_Id = message.Chat_Id, TextContent = await GetJoke(httpFactory) }); ;
                await context.Commit();
            }
        }
        private async Task<string> GetJoke(IHttpClientFactory httpClient)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "https://icanhazdadjoke.com/");
            request.Headers.Add("Accept", "text/plain");
            var client = httpClient.CreateClient();
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                return "There are no jokes today :(";
            }
        }
        private bool ContainsAnyBoryWords(string str)
        {
            var words = str.Split(' ');
            return words.Any(ss => BoryWords.Contains(ss));
        }
    }
}
