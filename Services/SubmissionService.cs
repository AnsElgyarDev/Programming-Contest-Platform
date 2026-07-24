using Microsoft.EntityFrameworkCore;
using Programming_Contest_Platform.Data;
using Programming_Contest_Platform.DTO;

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
                        userName = sub.User.UserName ?? "",
                        SubmissionStatus = sub.SubmissionState,
                        SubmittedAt = sub.SubmissionTime
                    })
                    .ToListAsync();
    }

    public async Task<ICollection<SubmissionSummaryDto>> GetUserRecentSubmissionsAsync(int userId)
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
}