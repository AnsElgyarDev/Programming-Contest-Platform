using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Identity.Client;
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

        app.MapGet("api/contests/{id:int}", async (int contestId) =>
        {
        });
    }

}