using Chat.Data.ContextConfigurations.MSSQLConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.Models
{
    public class MSSQLContext:DbContext,IDataContext
    {
        public DbSet<Chat> Chats { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ChatType> ChatTypes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatMember> ChatMembers { get; set; }

        private IDbContextTransaction _transaction;

        public MSSQLContext(DbContextOptions<MSSQLContext> options) : base(options)
        {

        }
        protected MSSQLContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            BuildMSSQLConfiguration(builder);
        }

        private void BuildMSSQLConfiguration(ModelBuilder builder)
        {
            new ChatConfiguration(builder.Entity<Chat>());
            new MessageConfiguration(builder.Entity<Message>());
            new ChatTypeConfiguration(builder.Entity<ChatType>());
            new UserConfiguration(builder.Entity<User>());
            new ChatMemberConfiguration(builder.Entity<ChatMember>());
        }

        public async Task BeginTransaction()
        {
            _transaction = await Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            try
            {
                SaveChanges();
                await _transaction.CommitAsync();
            }
            catch
            {
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
            }
        }

        public async Task Rollback()
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
    }
}
