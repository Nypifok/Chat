using Chat.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.ContextConfigurations.MSSQLConfiguration
{
    public class ChatTypeConfiguration
    {
        public ChatTypeConfiguration(EntityTypeBuilder<ChatType> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Type);
            entityBuilder.Property(t => t.Type)
                .HasMaxLength(20);
            entityBuilder.HasData(new ChatType { Type = "Dialog" },
                                  new ChatType { Type="Group Chat"});
        }
    }
}
