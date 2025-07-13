using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using unolink.domain.Models;

namespace unolink.infrastructure.Data
{
    public class OngTicketConfiguration : IEntityTypeConfiguration<OngTicket>
    {
        public void Configure(EntityTypeBuilder<OngTicket> builder)
        {
            builder.ToTable(nameof(OngTicket));

            builder.Property(x => x.Reviwed);

            builder.Property(x => x.Accepeted);

            builder.Property(x => x.Description)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Cep)
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(x => x.Cnpj)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(x => x.ExpirationDate);
        }
    }
}