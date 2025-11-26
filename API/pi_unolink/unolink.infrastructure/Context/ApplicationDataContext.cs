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
        public DbSet<PostComment> PostComment { get; set; }
        public DbSet<PostVotes> PostVotes { get; set;}
        public DbSet<CommentVotes> CommentVotes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDataContext).Assembly);

            modelBuilder.Entity<PostVotes>(eb =>
            {
                eb.HasKey(pv => new { pv.UserId, pv.PostId });

                eb.HasOne(pv => pv.User)
                    .WithMany(u => u.VotedPosts)
                    .HasForeignKey(pv => pv.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                eb.HasOne(pv => pv.Post)
                    .WithMany(v => v.Votes)
                    .HasForeignKey(pv => pv.PostId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<UserPost>(eb =>
            {
                eb.HasOne(up => up.User)
                    .WithMany(u => u.UserPosts)
                    .HasForeignKey(up => up.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CommentVotes>(eb =>
            {
                eb.HasKey(comVote => new { comVote.UserId, comVote.CommentId });

                eb.HasOne(comVote => comVote.User)
                    .WithMany() 
                    .HasForeignKey(comVote => comVote.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                eb.HasOne(comVote => comVote.Comment)
                    .WithMany()
                    .HasForeignKey(comVote => comVote.CommentId)
                    .OnDelete(DeleteBehavior.Cascade);

            });
        }
        
    }
}