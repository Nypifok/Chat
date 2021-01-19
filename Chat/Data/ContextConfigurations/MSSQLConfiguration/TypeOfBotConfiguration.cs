using Chat.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.ContextConfigurations.MSSQLConfiguration
{
    public class TypeOfBotConfiguration
    {
        public TypeOfBotConfiguration(EntityTypeBuilder<TypeOfBot> entityBuilder)
        {
            entityBuilder.Property(t => t.Title).HasMaxLength(30);
            entityBuilder.HasKey(t=>t.Title);
        }
    }
}
