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

    // public Task<ICollection<ShowContestDto>> ShowContest(int contestId)
    // {
    //     var a7a = _context.Contests
    //                       .AsNoTracking()
    //                       .Where(contest => contest.ContestId == contestId)
    //                       .Select(contestDto => new ContestDetailsDto
    //                       {
                              
    //                       });
    // }
}