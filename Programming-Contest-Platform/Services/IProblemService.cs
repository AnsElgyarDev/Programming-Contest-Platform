using Programming_Contest_Platform.DTO;

namespace Programming_Contest_Platform.Services;
public interface IProblemService
{
    public Task<ICollection<ProblemSummaryDto>> GetAllProblems();
    public Task<ProblemDetailsDto> GetProblem(int problemId);
} 