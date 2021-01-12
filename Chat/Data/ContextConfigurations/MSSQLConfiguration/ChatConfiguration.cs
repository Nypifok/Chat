using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Data.ContextConfigurations.MSSQLConfiguration
{
    public class ChatConfiguration
    {
        public ChatConfiguration(EntityTypeBuilder<Models.Chat> entityBuilder)
        {
            entityBuilder.HasKey(c => c.Id);
            entityBuilder.HasOne(c => c.ChatType)
                .WithMany(ct => ct.Chats)
                .HasForeignKey(c => c.Type)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            entityBuilder.Property(c => c.Title)
                .HasMaxLength(30)
                .IsRequired(false);
        }
    }
}
