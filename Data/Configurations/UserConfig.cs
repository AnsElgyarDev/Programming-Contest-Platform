using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Programming_Contest_Platform.Entity;

namespace Programming_Contest_Platform.Data.Configurations;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Email)
               .HasMaxLength(150)
               .IsRequired();
       
       builder.HasIndex(user => user.Email)
              .IsUnique();

        builder.Property(user => user.PasswordHash)
               .HasMaxLength(256)
               .IsRequired();

        builder.Property(user => user.UserName)
               .HasMaxLength(100)
               .IsRequired();
    }
}