using FullstackTest.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullstackTest.Persistence.EntityConfiguration
{
    class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);

            builder
                .Property(u => u.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(u => u.Name)
                .HasMaxLength(100)
                .IsRequired(false);

            builder
                .Property(u => u.Password)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
