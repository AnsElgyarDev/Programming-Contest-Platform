using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Programming_Contest_Platform.Entity;

namespace Programming_Contest_Platform.Data.Configurations;

public class ProblemConfig : IEntityTypeConfiguration<Problem>
{
    public void Configure(EntityTypeBuilder<Problem> builder)
    {
        builder.ToTable("Problems");

        builder.HasKey(p => p.ProblemId);

        builder.Property(p => p.ProblemName)
               .HasMaxLength(200)
               .IsRequired();

        builder.Property(p => p.ProblemLevel)
               .HasDefaultValue(800)
               .IsRequired();

        builder.HasOne(p => p.Contest)
               .WithMany(c => c.Problems)
               .HasForeignKey(p => p.ContestId)
               .IsRequired();
    }
}