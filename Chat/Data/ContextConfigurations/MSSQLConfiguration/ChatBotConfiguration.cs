using Chat.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.ContextConfigurations.MSSQLConfiguration
{
    public class ChatBotConfiguration
    {
        public ChatBotConfiguration(EntityTypeBuilder<ChatBot> entityBuilder)
        {

            entityBuilder.HasOne(cb => cb.Bot).WithMany(b => b.ChatBots).HasForeignKey(cb => cb.BotId);
            entityBuilder.HasOne(cb => cb.Chat).WithMany(c =>c.ChatBots).HasForeignKey(cb => cb.ChatId);
            entityBuilder.HasKey(cb => new { cb.BotId, cb.ChatId });
        }
    }
}
