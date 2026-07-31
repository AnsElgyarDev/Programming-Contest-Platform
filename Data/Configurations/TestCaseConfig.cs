using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Programming_Contest_Platform.Entity;

namespace Programming_Contest_Platform.Data.Configurations;

public class TestCaseConfig : IEntityTypeConfiguration<TestCase>
{
    public void Configure(EntityTypeBuilder<TestCase> builder)
    {
            builder.HasKey(tc => tc.Id);

            builder.Property(tc => tc.Input)
                  .IsRequired();

            builder.Property(tc => tc.ExpectedOutput)
                  .IsRequired();

            // Relation with Problem (One-to-Many)
            builder.HasOne(tc => tc.Problem)
                  .WithMany(p => p.TestCases)
                  .HasForeignKey(tc => tc.ProblemId)
                  .OnDelete(DeleteBehavior.Cascade);
    }
}