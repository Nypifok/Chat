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
        public DbSet<Bot> Bots { get; set; }
        public DbSet<ChatBot> ChatBots { get; set; }

        Task BeginTransaction();
        Task Commit();
        Task Rollback();
    }
}
