using Chat.Data.Dtos;
using Chat.Data.Models;
using Chat.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Services.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly IDataContext context;
        public MessageService(IDataContext context)
        {
            this.context = context;
        }
        public async Task<ServiceResponse<Message>> DeleteMessageAsync(MessageDeletingDto dto, Guid UserId)
        {

            await context.BeginTransaction();
            try
            {

                var message = await context.Messages.FindAsync(dto.MessageId);
                if (message != null && await context.Users.FindAsync(UserId) != null && message.AuthorId == UserId)
                {
                    context.Messages.Remove(message);
                    context.Commit();
                    return new ServiceResponse<Message> { Data = message };
                }
                context.Rollback();
                throw new Exception("U are not the owner");
            }
            catch (Exception ex)
            {
                context.Rollback();
                return new ServiceResponse<Message> { Message = ex.Message, Success = false };
            }

        }

        public async Task<ServiceResponse<Message>> EditMessageAsync(MessageEditingDto dto, Guid UserId)
        {
            await context.BeginTransaction();
            try
            {
                var message = await context.Messages.FindAsync(dto.MessageId);
                if (message != null && await context.Users.FindAsync(UserId) != null && message.AuthorId == UserId)
                {
                    message.TextContent = dto.MessageContent;
                    message.IsEdited = true;
                    context.Messages.Update(message);
                    context.Commit();
                    return new ServiceResponse<Message> { Data = message };
                }
                context.Rollback();
                throw new Exception("U are not the owner");
            }
            catch (Exception ex)
            {
                context.Rollback();
                return new ServiceResponse<Message> { Message = ex.Message, Success = false };
            }
        }

        public async Task<ServiceResponse<IEnumerable<Message>>> ReadMessagesAsync(ChatReadingDto dto, Guid UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<Message>> SendMessageAsync(MessageSendingDto dto, Guid UserId)
        {
            await context.BeginTransaction();
            try
            {

                if (await context.Users.FindAsync(UserId) != null &&
                    await context.Chats.FindAsync(dto.ChatId) != null &&
                    await context.ChatMembers.FindAsync(dto.ChatId, UserId) != null
                    )
                {
                    var message = new Message { SendingTime = DateTime.Now, TextContent = dto.MessageContent, AuthorId = UserId, Chat_Id = dto.ChatId };
                    await context.Messages.AddAsync(message);
                    context.Commit();
                    return new ServiceResponse<Message> { Data = message };
                }
                context.Rollback();
                throw new Exception("U are not the owner");
            }
            catch (Exception ex)
            {
                context.Rollback();
                return new ServiceResponse<Message> { Message = ex.Message, Success = false };
            }
        }
    }
}
