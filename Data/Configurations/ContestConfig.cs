using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Programming_Contest_Platform.Entity;

namespace Programming_Contest_Platform.Data.Configurations;

public class ContestConfig : IEntityTypeConfiguration<Contest>
{
    public void Configure(EntityTypeBuilder<Contest> builder)
    {
       builder.ToTable("Contests");

       builder.HasKey(c => c.ContestId);

       builder.Property(c => c.ContestName)
              .HasMaxLength(100)
              .IsRequired();
       
       builder.Property(c => c.Languages);

       builder.Property(c => c.ContestLevel)
              .IsRequired();

       builder.Property(c => c.ContestStartTime)
              .IsRequired();

       builder.Property(c => c.ContestEndTime)
              .IsRequired();
    }
}