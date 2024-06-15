using Microsoft.EntityFrameworkCore;
using SocialMediaAppAPI.Models;

namespace SocialMediaAppAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserLikedPost> UserLikedPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired();

            modelBuilder.Entity<Post>()
                .Property(p => p.Title)
                .IsRequired();
            modelBuilder.Entity<Post>()
                .Property(p => p.Description)
                .IsRequired();

            modelBuilder.Entity<UserLikedPost>()
                .HasKey(up => new { up.PostId, up.UserId });
            modelBuilder.Entity<UserLikedPost>()
                .HasOne(p => p.Post)
                .WithMany(up => up.UserLikedPosts)
                .HasForeignKey(p => p.PostId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserLikedPost>()
                .HasOne(u => u.User)
                .WithMany(up => up.UserLikedPosts)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
