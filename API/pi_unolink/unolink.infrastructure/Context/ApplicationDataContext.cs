using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using unolink.domain.Models;

namespace unolink.infrastructure.Context
{
    public class ApplicationDataContext : IdentityDbContext<User,IdentityRole<Guid>,Guid>
    {
        public ApplicationDataContext(DbContextOptions options) : base(options) { }
        public DbSet<OngTicket> OngTicket { get; set; }
        public DbSet<UserPost> UserPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDataContext).Assembly);
        }
        
    }
}