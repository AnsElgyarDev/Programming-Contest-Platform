using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Programming_Contest_Platform.Data;
using Programming_Contest_Platform.DTO;

namespace Programming_Contest_Platform.Services;

public class ProblemService : IProblemService
{
    private readonly AppDbContext _context;
    private readonly IMemoryCache _memoryCache;
    public ProblemService(AppDbContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _memoryCache = memoryCache;
    }
    public async Task<ICollection<ProblemSummaryDto>> GetAllProblems()
    {
        return await _context.Problems.AsNoTracking()
                    .Select(problem => new ProblemSummaryDto
                    {
                        ProblemName = problem.ProblemName,
                        ProblemLevel = problem.ProblemLevel 
                    }).ToListAsync();
    }
    public async Task<ProblemDetailsDto?> GetProblem(int problemId)
    {
        string cacheKey = $"Problem_{problemId}";

        return await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            entry.SlidingExpiration = TimeSpan.FromMinutes(10);
            entry.Priority = CacheItemPriority.High;

            var problemDto = await _context.Problems
                .Where(p => p.ProblemId == problemId)
                .Select(p => new ProblemDetailsDto
                {
                    ProblemName = p.ProblemName,
                    ProblemDescription = p.ProblemDescription,
                    ProblemLevel = p.ProblemLevel,
                    ContestName = p.Contest.ContestName
                })
                .FirstOrDefaultAsync();

            if (problemDto is null)
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);
                return null;
            }

            return problemDto;
        });
    }
}