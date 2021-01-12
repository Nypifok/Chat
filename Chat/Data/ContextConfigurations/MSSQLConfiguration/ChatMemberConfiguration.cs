using Chat.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.ContextConfigurations.MSSQLConfiguration
{
    public class ChatMemberConfiguration
    {
        public ChatMemberConfiguration(EntityTypeBuilder<ChatMember> entityBuilder)
        {         
            entityBuilder.HasOne(cm => cm.User)
                .WithMany(u => u.ChatMembers)
                .HasForeignKey(cm => cm.UserId)
                .IsRequired();
            entityBuilder.HasOne(cm => cm.Chat)
               .WithMany(c => c.ChatMembers)
               .HasForeignKey(cm => cm.ChatId)
               .IsRequired();
            entityBuilder.HasKey(cm => new { cm.ChatId, cm.UserId });

        }
    }
}
