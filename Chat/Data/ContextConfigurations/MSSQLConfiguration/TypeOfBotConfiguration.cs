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
            entityBuilder.HasKey(t=>t.Title);
        }
    }
}
