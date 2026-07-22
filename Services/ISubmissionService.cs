using Programming_Contest_Platform.DTO;
namespace Programming_Contest_Platform.Services;

public interface ISubmissionService
{
    public Task<ICollection<SubmissionSummaryDto>> GetUserRecentSubmissionsAsync(int userId);
    public Task<ICollection<ProblemSubmissionsDto>> GetProblemSubmissionsAsync(int ProblemId);

}