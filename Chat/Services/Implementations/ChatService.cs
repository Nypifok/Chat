using Chat.Data.Dtos;
using Chat.Data.Models;
using Chat.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Services.Implementations
{
    public class ChatService : IChatService
    {
        private readonly IDataContext context;
        public ChatService(IDataContext context)
        {
            this.context = context;
        }
        public async Task<ServiceResponse<IEnumerable<ChatMember>>> AddToChatAsync(ChatActionDto dto, Guid UserId)
        {
            await context.BeginTransaction();
            try
            {
                var usersId = dto.TargetUsers.Select(u => u.Id);
                var users = context.Users.Where(u => usersId.Contains(u.Id)).ToList();
                var usersCount = users.Count;
                var chatMembers = new List<ChatMember>();
                var inviteMessages = new List<Message>(usersCount);

                foreach (User usr in users)
                {
                    chatMembers.Add(new ChatMember { UserId = usr.Id, ChatId = dto.TargetChatId });
                    inviteMessages.Add(new Message { IsSystemMessage = true, AuthorId = UserId, Chat_Id = dto.TargetChatId, SendingTime = DateTime.Now, TextContent = $"New member @{usr.Id}" });
                }
                await context.ChatMembers.AddRangeAsync(chatMembers);

                await context.Messages.AddRangeAsync(inviteMessages);
                context.Commit();
                return new ServiceResponse<IEnumerable<ChatMember>> { Data = chatMembers };
            }
            catch (Exception ex)
            {

                context.Rollback();
                return new ServiceResponse<IEnumerable<ChatMember>> { Message = ex.Message, Success = false };
            }
        }

        public async Task<ServiceResponse<Data.Models.Chat>> CreateNewChatAsync(ChatCreatingDto dto, Guid UserId)
        {
            await context.BeginTransaction();
            try
            {
                var chat = new Data.Models.Chat { ChatType = context.ChatTypes.Find("Group Chat"), Title = dto.ChatTitle };

                await context.Chats.AddAsync(chat);
                await context.ChatMembers.AddAsync(new ChatMember { ChatId = chat.Id, UserId = UserId, IsOwner = true });
                context.Commit();
                return new ServiceResponse<Data.Models.Chat> { Data = chat };

            }
            catch (Exception ex)
            {
                context.Rollback();
                return new ServiceResponse<Data.Models.Chat> { Message = ex.Message, Success = false };
            }

        }

        public async Task<ServiceResponse<Data.Models.Chat>> CreateNewDialogAsync(DialogCreatingDto dto, Guid UserId)
        {
            await context.BeginTransaction();
            try
            {
                var chat = new Data.Models.Chat { ChatType = context.ChatTypes.Find("Dialog") };

                await context.Chats.AddAsync(chat);
                if (context.Users.Find(UserId) != null && context.Users.Find(UserId) != null)
                {
                    await context.ChatMembers.AddAsync(new ChatMember { ChatId = chat.Id, UserId = UserId, IsOwner = true });
                    await context.ChatMembers.AddAsync(new ChatMember { ChatId = chat.Id, UserId = dto.TargetUserId, IsOwner = true });
                }
                context.Commit();
                return new ServiceResponse<Data.Models.Chat> { Data = chat };

            }
            catch (Exception ex)
            {
                context.Rollback();
                return new ServiceResponse<Data.Models.Chat> { Message = ex.Message, Success = false };
            }

        }

        public async Task<ServiceResponse<IEnumerable<ChatMember>>> DeleteFromChatAsync(ChatActionDto dto, Guid UserId)
        {
            await context.BeginTransaction();
            try
            {
                var user =await context.Users.FindAsync(UserId);
                var members = new List<ChatMember>();
                if (user != null)
                {
                    var whoRequested=await context.ChatMembers.FindAsync(dto.TargetChatId,UserId );
                    if (whoRequested.IsOwner)
                    {
                        var removeMessages = new List<Message>();
                        var membersId = dto.TargetUsers.Select(t => t.Id).ToList();
                        members = await context.ChatMembers.Where(cm => cm.ChatId == dto.TargetChatId &&membersId.Contains(cm.UserId)).ToListAsync();
                        context.ChatMembers.RemoveRange(members);
                        foreach(var member in members)
                        {
                            removeMessages.Add(new Message { IsSystemMessage = true, AuthorId = UserId, Chat_Id = dto.TargetChatId, SendingTime = DateTime.Now, TextContent = $"@{member.UserId} was removed" });
                        }
                        await context.Messages.AddRangeAsync(removeMessages);
                        context.Commit();
                        return new ServiceResponse<IEnumerable<ChatMember>> { Data = members };
                    }
                }

                context.Rollback();
                return new ServiceResponse<IEnumerable<ChatMember>> { Message = "U are not the owner", Success = false };

            }
            catch (Exception ex)
            {
                context.Rollback();
                return new ServiceResponse<IEnumerable<ChatMember>> { Message = ex.Message, Success = false };
            }
        }

    }
}
