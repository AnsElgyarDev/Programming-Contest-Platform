using Microsoft.EntityFrameworkCore;
using Programming_Contest_Platform.Data;
using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Entity;
using Programming_Contest_Platform.Helper;

namespace Programming_Contest_Platform.Services;

public class ContestService : IContestService
{
    private readonly AppDbContext _context = null!;
    public ContestService(AppDbContext context)
    {
        this._context = context;
    }
    public async Task<ICollection<ShowContestDto>> ShowAllContests()
    {
            var now = DateTime.UtcNow;

            return await _context!.Contests
                .AsNoTracking() 
                .Where(c => c.ContestEndTime > now)
                .Select(c => new ShowContestDto 
                {
                    ContestName = c.ContestName,
                    contestStartTime = c.ContestStartTime,
                    contestEndTime = c.ContestEndTime
                })
                .ToListAsync();
    }

    public async Task<ContestDetailsDto?> ShowContest(int contestId)
    {
            return await _context.Contests
                        .AsNoTracking()
                        .Where(contest => contest.ContestId == contestId)
                        .Select(contest => new ContestDetailsDto
                        {
                            ContestName = contest.ContestName,
                            ContestLevel = contest.ContestLevel,
                            Problems = contest.Problems.Select(problem => new ProblemSummaryDto
                            {
                                ProblemName = problem.ProblemName,
                                ProblemLevel = problem.ProblemLevel
                            }).ToList()
                        })
                        .FirstOrDefaultAsync();
    }

    public async Task<List<string>> ShowContestLanguage(int contestId)
    {
        var contest = await _context.Contests.FirstOrDefaultAsync(contest => contest.ContestId == contestId);
        
        if(contest is null)
            return null!;

        return contest.Languages;
    }

}