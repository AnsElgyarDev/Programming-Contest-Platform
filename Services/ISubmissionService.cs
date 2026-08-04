using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Entity;
namespace Programming_Contest_Platform.Services;

public interface ISubmissionService
{
    // Get methods 
    public Task<ICollection<SubmissionSummaryDto>> GetUserRecentSubmissionsAsync(Guid userId);
    public Task<ICollection<ProblemSubmissionsDto>> GetProblemSubmissionsAsync(int ProblemId);
    
    // Post Methods 
    public Task<ServiceResult<int>> SubmitProblem(ProblemSubmissionDto problemDto);
}