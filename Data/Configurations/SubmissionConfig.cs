using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Programming_Contest_Platform.Entity;

namespace Programming_Contest_Platform.Data.Configurations;

public class SubmissionConfig : IEntityTypeConfiguration<Submission>
{
    public void Configure(EntityTypeBuilder<Submission> builder)
    {
        builder.ToTable("Submissions");

        builder.HasKey(sub => sub.SubmissionId);

        builder.Property(sub => sub.SubmissionCode)
               .IsRequired();

        builder.Property(sub => sub.SubmissionTime)
               .IsRequired();

        builder.HasOne(sub => sub.User)
               .WithMany(user => user.Submissions)
               .HasForeignKey(sub => sub.UserId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.HasOne(sub => sub.Problem)
               .WithMany(problem => problem.Submissions)
               .HasForeignKey(sub => sub.ProblemId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}