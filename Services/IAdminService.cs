using  Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Entity;
namespace Programming_Contest_Platform.Services;

public interface IAdminService
{
    // Creating and removing and updating Problems 
    public Task<ServiceResult> CreateProblem(Problem problem);
    public Task<ServiceResult> UpdateProblem(int problemId, ProblemDetailsDto problemDetailsDto);
    public Task<ServiceResult> RemoveProblem(int problemId);

}