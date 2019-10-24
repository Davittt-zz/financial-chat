using JobsityFinancialChat.Domain.Models.DB;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace JobsityFinancialChat.Domain.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, AppRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUserChatroom>()
            .HasKey(bc => new { bc.ApplicationUserId, bc.ChatRoomId });
            builder.Entity<ApplicationUserChatroom>()
                .HasOne(bc => bc.ApplicationUser)
                .WithMany(b => b.Chatrooms)
                .HasForeignKey(bc => bc.ApplicationUserId);
            builder.Entity<ApplicationUserChatroom>()
                .HasOne(bc => bc.ChatRoom)
                .WithMany(c => c.Members)
                .HasForeignKey(bc => bc.ChatRoomId);

            base.OnModelCreating(builder);
        }

        public DbSet<ChatRoom> ChatRooms { get; set; }

        public DbSet<Message> Messages { get; set; }


    }
}