using Chat.Data.Dtos;
using Chat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Services.Interfaces
{
    public interface IMessageService
    {
        Task<ServiceResponse<Message>> SendMessageAsync(MessageSendingDto dto, Guid UserId);
        Task<ServiceResponse<Message>> DeleteMessageAsync(MessageDeletingDto dto, Guid UserId);
        Task<ServiceResponse<Message>> EditMessageAsync(MessageEditingDto dto, Guid UserId);
        Task<ServiceResponse<IEnumerable<Message>>> ReadMessagesAsync(ChatReadingDto dto, Guid UserId);
    }
}
