using Chat.Data.Dtos;
using Chat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Services.Interfaces
{
    public interface IChatService
    {
        Task<ServiceResponse<IEnumerable<ChatMember>>> AddToChatAsync(ChatActionDto dto, Guid UserId);
        Task<ServiceResponse<Data.Models.Chat>> CreateNewDialogAsync(DialogCreatingDto dto, Guid UserId);
        Task<ServiceResponse<Data.Models.Chat>> CreateNewChatAsync(ChatCreatingDto dto, Guid UserId);
        Task<ServiceResponse<IEnumerable<ChatMember>>> DeleteFromChatAsync(ChatActionDto dto, Guid UserId);
    }
}
