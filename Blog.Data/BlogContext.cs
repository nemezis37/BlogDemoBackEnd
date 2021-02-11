using System.Linq;
using Blog.Model;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    public class BlogContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public BlogContext(DbContextOptions<BlogContext> options) :
            base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var mutableForeignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                mutableForeignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            ConfigureModelBuilderForUser(modelBuilder);
        }

        void ConfigureModelBuilderForUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>()
                .Property(user => user.UserName)
                .HasMaxLength(60)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(user => user.Email)
                .HasMaxLength(60)
                .IsRequired();
        }
    }
}