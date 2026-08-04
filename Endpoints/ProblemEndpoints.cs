using Microsoft.AspNetCore.Http.HttpResults;
using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Services;

namespace Programming_Contest_Platform.Endpoints;

public static class ProblemEndpoints
{
    public static async Task UseProblemEndpoints(this WebApplication app)
    {
        
        var group = app.MapGroup("api/problems");
        
        group.MapGet("", async Task<Results<NotFound<string>, Ok<ICollection<ProblemSummaryDto>>>>
        (IProblemService problemService) =>
        {
            var problems = await problemService.GetAllProblems();
            
            if(problems is null)
                return TypedResults.NotFound("There are No Problems At this Moment!");
            
            return TypedResults.Ok(problems);
        });

         group.MapGet("/{ProblemId}", async Task<Results<NotFound<string>, Ok<ProblemDetailsDto>>>
        (IProblemService problemService, int ProblemId) =>
        {
            var problem = await problemService.GetProblem(ProblemId);
            
            if(problem is null)
                return TypedResults.NotFound("There is No Problem with this Id!");
            
            return TypedResults.Ok(problem);
        });
        
    }
}