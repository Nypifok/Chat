﻿using Chat.Data.ContextConfigurations.MSSQLConfiguration;
using Chat.Services.Implementations;
using Chat.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace Chat.Data.Models
{
    public class MSSQLContext : IdentityDbContext<User,UserRole,Guid>, IDataContext
    {
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatType> ChatTypes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatMember> ChatMembers { get; set; }
        public DbSet<Bot> Bots { get; set; }
        public DbSet<ChatBot> ChatBots { get; set; }
        public DbSet<TypeOfBot> TypesOfBots { get ; set; }
        public DbSet<BotType> BotTypes { get; set; }

        private IDbContextTransaction _transaction;
        private IBotNotificator botNotificator;

        public MSSQLContext(DbContextOptions<MSSQLContext> options) : base(options)
        {
            botNotificator = this.GetInfrastructure().GetRequiredService<IBotNotificator>();
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
            new BotConfiguration(builder.Entity<Bot>());
            new ChatBotConfiguration(builder.Entity<ChatBot>());
            new TypeOfBotConfiguration(builder.Entity<TypeOfBot>());
            new BotTypeConfiguration(builder.Entity<BotType>());
        }

        public async Task BeginTransaction()
        {
            _transaction = await Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            try
            {
                await SaveChangesAsync();
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
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var entityEntry in ChangeTracker.Entries<Message>())
            {
                if (entityEntry.State == EntityState.Added&&entityEntry.Entity.BotId==null)
                {
                    
                    botNotificator.Notificate(entityEntry.Entity);  
                }
            }

            try
            {
                return await base.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
            finally
            {
            }
        }
    }
}
