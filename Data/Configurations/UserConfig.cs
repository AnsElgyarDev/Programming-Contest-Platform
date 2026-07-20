using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Programming_Contest_Platform.Entity;

namespace Programming_Contest_Platform.Data.Configurations;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(user => user.UserId);

        builder.Property(user => user.UserEmail)
               .HasMaxLength(150)
               .IsRequired();
       
       builder.HasIndex(user => user.UserEmail)
              .IsUnique();

        builder.Property(user => user.UserPassword)
               .HasMaxLength(256)
               .IsRequired();

        builder.Property(user => user.UserName)
               .HasMaxLength(100)
               .IsRequired();
    }
}