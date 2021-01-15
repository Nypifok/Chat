using Chat.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data.ContextConfigurations.MSSQLConfiguration
{
    public class UserConfiguration
    {
        public UserConfiguration(EntityTypeBuilder<User> entityBuilder)
        {
            entityBuilder.Property(u => u.Tag)
                .HasMaxLength(20)
                .IsRequired();
            entityBuilder.HasIndex(u => u.Tag)
                .IsUnique();
            entityBuilder.Property(u => u.Name)
                .HasMaxLength(30)
                .IsRequired();

        }
    }
}
