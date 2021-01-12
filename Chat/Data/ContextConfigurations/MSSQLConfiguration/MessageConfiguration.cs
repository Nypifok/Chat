using Chat.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Chat.Data.ContextConfigurations.MSSQLConfiguration
{
    public class MessageConfiguration
    {
        public MessageConfiguration(EntityTypeBuilder<Message> entityBuilder)
        {
            entityBuilder.HasKey(m => m.Id);
            entityBuilder.HasOne(m => m.Author)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.AuthorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            entityBuilder.HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.Chat_Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            entityBuilder.Property(m => m.TextContent)
                .HasColumnType("varchar")
                .HasMaxLength(7000)
                .IsRequired()
                .HasDefaultValue("");
            entityBuilder.Property(m => m.SendingTime)
                .IsRequired();
        }
    }
}
