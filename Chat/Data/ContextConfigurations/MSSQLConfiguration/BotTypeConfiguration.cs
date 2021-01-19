using Chat.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.ContextConfigurations.MSSQLConfiguration
{
    public class BotTypeConfiguration
    {
        public BotTypeConfiguration(EntityTypeBuilder<BotType> entityBuilder)
        {
            entityBuilder.HasOne(bt => bt.Bot)
                .WithMany(b => b.BotTypes)
                .HasForeignKey(bt => bt.BotId);
            entityBuilder.HasOne(bt => bt.Type)
                .WithMany(tb => tb.BotTypes)
                .HasForeignKey(bt => bt.TypeId);

            entityBuilder.HasKey(bt=>new {bt.BotId,bt.TypeId });
        }
    }
}
