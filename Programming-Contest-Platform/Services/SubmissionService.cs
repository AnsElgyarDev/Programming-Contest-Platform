using Microsoft.EntityFrameworkCore;
using Programming_Contest_Platform.Data;
using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Entity;

namespace Programming_Contest_Platform.Services;

public class SubmissionService : ISubmissionService
{
    private readonly AppDbContext _context;
    public SubmissionService(AppDbContext context)
    {
        this._context = context;
    }

    public async Task<ICollection<ProblemSubmissionsDto>> GetProblemSubmissionsAsync(int ProblemId)
    {
        return await _context.Submissions
                    .AsNoTracking()
                    .Where(sub => sub.ProblemId == ProblemId) 
                    .Select(sub => new ProblemSubmissionsDto     
                    {
                        userName = sub.User.Username ?? "",
                        SubmissionStatus = sub.SubmissionState,
                        SubmittedAt = sub.SubmissionTime
                    })
                    .ToListAsync();
    }

    public async Task<ICollection<SubmissionSummaryDto>> GetUserRecentSubmissionsAsync(Guid userId)
    {
        return await _context.Submissions
                                .AsNoTracking()
                                .Where(user => user.UserId == userId)
                                .OrderByDescending(user => user.SubmissionTime)
                                .Select(submission => new SubmissionSummaryDto
                                {
                                    ProblemId = submission.ProblemId,
                                    SubmissionId = submission.SubmissionId,
                                    ProblemName = submission.Problem.ProblemName,
                                    ProblemStatus = submission.SubmissionState.ToString(),
                                    SubmittedAt = submission.SubmissionTime
                                }).
                                Take(10).
                                ToListAsync();
    }

    public async Task<ServiceResult<int>> SubmitProblem(ProblemSubmissionDto problemDto)
    {
        var problem = await _context.Problems
            .FirstOrDefaultAsync(p => p.ProblemId == problemDto.problemId);

        if (problem == null)
            return ServiceResult<int>.Failure("Problem not found.");
            
        string initialStatus = string.IsNullOrWhiteSpace(problemDto.Code) 
            ? "Wrong Answer" 
            : "Accepted";

        var submission = new Submission
        {
            ProblemId = problemDto.problemId,
            UserId = problemDto.userId,
            SubmissionCode = problemDto.Code,
            Language = problemDto.Language,
            SubmissionState = initialStatus,
            SubmissionTime = DateTime.UtcNow
        };

        _context.Submissions.Add(submission);
        await _context.SaveChangesAsync();

        return ServiceResult<int>.Success(submission.SubmissionId); 
    }
}