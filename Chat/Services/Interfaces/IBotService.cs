using Chat.Data.Dtos;
using Chat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Services.Interfaces
{
    public interface IBotService
    {
        Task<ServiceResponse<Bot>> CreateBotAsync(BotCreatingDto dto);
        Task<ServiceResponse<Message>> DeleteBotAsync(BotDeletingDto dto);
    }
}
