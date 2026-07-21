using  Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Entity;
namespace Programming_Contest_Platform.Services;

public interface IContestService
{
    // Get All The Available Contests
    public Task<ICollection<ShowContestDto>> ShowAllContests();
    public Task<ContestDetailsDto> ShowContest(int contestId);
    
}