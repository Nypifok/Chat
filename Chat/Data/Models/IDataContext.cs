using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.Models
{
    public interface IDataContext
    {
        DbSet<Chat> Chats { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<ChatType> ChatTypes { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<ChatMember> ChatMembers { get; set; }
        DbSet<Bot> Bots { get; set; }
        DbSet<ChatBot> ChatBots { get; set; }
        DbSet<TypeOfBot> TypesOfBots { get; set; }
        DbSet<BotType> BotTypes { get; set; }

        Task BeginTransaction();
        Task Commit();
        Task Rollback();
    }
}
