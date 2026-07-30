using Microsoft.EntityFrameworkCore;
using Programming_Contest_Platform.Data;
using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Entity;

namespace Programming_Contest_Platform.Services;

public class AdminService : IAdminService
{
    private readonly AppDbContext _context;
    public AdminService(AppDbContext context)
    {
        this._context = context;
    }
    public async Task<ServiceResult> CreateProblem(Problem problem)
    {
        if(problem is null)
            return new ServiceResult
            {
                ErrorMessage = "There is no Problem"  
            };

        await _context.Problems.AddAsync(problem);
        await _context.SaveChangesAsync();
        
        return new ServiceResult
        {
            isSuccess = true  
        };
    }

    public async Task<ServiceResult> RemoveProblem(int problemId)
    {
        var problem = await _context.Problems.FindAsync(problemId);

        if(problem is null)
            return new ServiceResult
            {
                ErrorMessage = "Not found!"
            };

        return new ServiceResult
        {
            isSuccess = true
        };
    }

public async Task<ServiceResult> UpdateProblem(int problemId, ProblemDetailsDto problemDetailsDto)
{
    var problem = await _context.Problems.FindAsync(problemId);

    if (problem == null)
    {
        return ServiceResult.Failure($"Problem with ID {problemId} was not found.");
    }

    problem.ProblemName = problemDetailsDto.ProblemName ?? problem.ProblemName;
    problem.ProblemDescription = problemDetailsDto.ProblemDescription ?? problem.ProblemDescription;

    if (problemDetailsDto.ContestId != 0)
    {
        var contestExists = await _context.Contests.AnyAsync(c => c.ContestId == problemDetailsDto.ContestId);

        if (!contestExists)
        {
            return ServiceResult.Failure($"Contest with ID {problemDetailsDto.ContestId} does not exist.");
        }

        problem.ContestId = problemDetailsDto.ContestId;
    }

    await _context.SaveChangesAsync();

    return ServiceResult.Success("");
}
}