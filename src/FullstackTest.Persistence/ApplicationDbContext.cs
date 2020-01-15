using System;
using FullstackTest.Domain;
using FullstackTest.Persistence.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace FullstackTest.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            
            Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData
            (
                new User()
                {
                    Id = Guid.Parse("a550ee91-d096-4d8d-af6f-14049ab5eb48"), 
                    Email = "testuser1@gmail.com", 
                    Name = "testUser1", 
                    Password = "123123",
                }
            );
        }
    }
}
