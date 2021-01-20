using Chat.Bots.FunctionalityExtensions;
using Chat.Data.Models;
using Chat.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Bots
{
    public class BotBuilder : IBotBuilder
    {
        private readonly IServiceProvider serviceProvider;
        public List<BaseBot> BotsCollection { get; set; }
        public BotBuilder(IBotNotificator botNotificator, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            BotsCollection = new List<BaseBot>();
            botNotificator.OnMessageSended += OnMessageSendedEventHandler;
        }



        public async Task BuildAndNotificateBots(IEnumerable<Guid> bots,Message message)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetService<IDataContext>();
            await context.BeginTransaction();
            var ids = bots;
            var botsConfigurations = context.BotTypes.Where(bt => ids.Contains(bt.BotId)).ToList();
            foreach (Guid id in ids)
            {
                if (BotsCollection.FirstOrDefault(b=>b.BotId==id) == null)
                {
                    var types = botsConfigurations.Where(bc => bc.BotId == id).Select(bt => bt.TypeId).ToList();

                    BaseBot bot;
                    bot = new StandartBot(id);

                    foreach (string str in types)
                    {

                        bot = Activator.CreateInstance(Type.GetType(str, true), bot) as BaseBot;

                    }
                    BotsCollection.Add(bot);
                }

            }
            BotsCollection.Where(b => ids.Contains(b.BotId)).ToList().ForEach(b => b.HandleMessage(message,serviceProvider));
            context.Commit();
        }
        public async void OnMessageSendedEventHandler(Message message)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetService<IDataContext>();
            await context.BeginTransaction();
            var botsId = context.ChatBots.Where(cb => cb.ChatId == message.Chat_Id).Select(cb => cb.BotId).ToList();
            BuildAndNotificateBots(botsId,message);
            context.Commit();
        }

    }
}
