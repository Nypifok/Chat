using Chat.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.ContextConfigurations.MSSQLConfiguration
{
    public class BotConfiguration
    {
        public BotConfiguration(EntityTypeBuilder<Bot> entityBuilder)
        {
            entityBuilder.HasKey(b => b.Id);
            entityBuilder.HasIndex(b => b.Title).IsUnique();
        }
    }
}
