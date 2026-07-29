using Microsoft.EntityFrameworkCore;
using Programming_Contest_Platform.Data;
using Programming_Contest_Platform.DTO;

namespace Programming_Contest_Platform.Services;

public class ProblemService : IProblemService
{
    private readonly AppDbContext _context;
    public ProblemService(AppDbContext context)
    {
        _context = context;
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

    public async Task<ProblemDetailsDto> GetProblem(int problemId)
    {
        var problem = await _context.Problems
                                .FindAsync(problemId);

        return new ProblemDetailsDto 
        {
            ProblemName = problem!.ProblemName,
            ProblemDescription = problem.ProblemDescription,
            ProblemLevel = problem.ProblemLevel,
            ContestName = problem.Contest.ContestName
        };
    }
}