using Chat.Data.Dtos;
using Chat.Data.Models;
using Chat.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Services.Implementations
{
    public class BotService : IBotService
    {
        private readonly IDataContext context;
        public BotService(IDataContext context)
        {
            this.context = context;
        }
        public async Task<ServiceResponse<Bot>> CreateBotAsync(BotCreatingDto dto)
        {
            await context.BeginTransaction();
            try
            {
                var bot = new Bot { Id = Guid.NewGuid(), Title = dto.Tilte };
                await context.Bots.AddAsync(bot);
                foreach (TypeOfBot type in dto.Types)
                {
                    if (context.TypesOfBots.Any(t => t.Title == type.Title))
                    {
                        context.BotTypes.Add(new BotType { BotId = bot.Id, TypeId = type.Title, Id = Guid.NewGuid() });
                    }
                }
                await context.Commit();
                return new ServiceResponse<Bot> { Data = bot };
            }
            catch (Exception ex)
            {

                context.Rollback();
                return new ServiceResponse<Bot> { Message = ex.Message, Success = false };
            }
        }

        public async Task<ServiceResponse<Message>> DeleteBotAsync(BotDeletingDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
