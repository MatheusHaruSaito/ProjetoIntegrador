using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using unolink.domain.Models;

namespace unolink.infrastructure.Data
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            builder.Property(x => x.UserName)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(266)
                .IsRequired();

            builder.Property(x => x.PasswordHash)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(266);

            builder.Property(x => x.Cep)
                .HasMaxLength(10);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(1);

        }
    }
}