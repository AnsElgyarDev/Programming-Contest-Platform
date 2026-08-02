using Microsoft.AspNetCore.Http.HttpResults;
using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Services;

namespace Programming_Contest_Platform.Endpoints;

public static class ContestEndpoints
{
    public static async Task UseContestEndpoints(this WebApplication app)
    {
        // Get Endpoints
        app.MapGet("api/contests", async Task<Results<NotFound<string>, Ok<ICollection<ShowContestDto>>>>
                  (IContestService contestService) =>
        {
            var contests = await contestService.ShowAllContests();
            
            if(contests is null)
                return TypedResults.NotFound("There is No Available Contests At this moment");
            
            return TypedResults.Ok(contests);
        });

        app.MapGet("api/contests/{contestId:int}", async Task<Results<NotFound<string>, Ok<ContestDetailsDto>>>
                  (IContestService contestService, int contestId) =>
        {
            var contest = await contestService.ShowContest(contestId);
            
            if(contest is null)
            {
                return TypedResults.NotFound("Contest Not Found");
            }

            return TypedResults.Ok(contest);
        });

        app.MapGet("/api/contests/{contestId:int}/Languages",(int contestId, IContestService contestService) =>
        {
            var Languages = contestService.ShowContestLanguage(contestId);
        });
        
    }

}