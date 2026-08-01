using  Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Entity;
namespace Programming_Contest_Platform.Services;

public interface IContestStandingsService
{
    // in-memory Methods phase
    Task UpdateUserScoreAsync(int contestId, string userId, int problemId, bool isAccepted, int penaltyTime);
    Task<IEnumerable<ContestantStandingDto>> GetLiveStandingsAsync(int contestId, int page = 1, int pageSize = 50);
    
    // Finalize the Contest And Calculate the Results in Runtime and Save in the DB   
    Task FinalizeContestStandingsAsync(int contestId);

    // Readonly method To Get The Contetst Result After the Contest Ends
    Task<IEnumerable<ContestantStandingDto>> GetHistoricalStandingsAsync(int contestId, int page = 1, int pageSize = 50);
}