using Microsoft.EntityFrameworkCore;
using Programming_Contest_Platform.Data;
using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Entity;
using Programming_Contest_Platform.Helper;

namespace Programming_Contest_Platform.Services;

public class ContestStandingsService : IContestStandingsService
{
    private readonly AppDbContext _context = null!;
    public ContestStandingsService(AppDbContext context)
    {
        this._context = context;
    }

    public Task FinalizeContestStandingsAsync(int contestId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ContestantStandingDto>> GetHistoricalStandingsAsync(int contestId, int page = 1, int pageSize = 50)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ContestantStandingDto>> GetLiveStandingsAsync(int contestId, int page = 1, int pageSize = 50)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserScoreAsync(int contestId, string userId, int problemId, bool isAccepted, int penaltyTime)
    {
        throw new NotImplementedException();
    }
}