using Microsoft.AspNetCore.Http.HttpResults;
using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Entity;
using Programming_Contest_Platform.Services;

namespace Programming_Contest_Platform.Endpoints;

public static class SubmissionEndpoints
{
    public static async Task UseSubmissionEndpoints(this WebApplication app)
    {
        app.MapGet("/api/users/{UserId:int}/recentSubmissions", async Task<Results<NotFound<string>, Ok<ICollection<SubmissionSummaryDto>>>> 
                 (ISubmissionService submissionService, int UserId) =>
        {
            var recentUserSubmissions = await submissionService.GetUserRecentSubmissionsAsync(UserId);
            
            if(recentUserSubmissions is null)
            {
                return TypedResults.NotFound("There are no Recent Submissions!");
            }

            return TypedResults.Ok(recentUserSubmissions);
        });

        app.MapGet("api/problems/{problemId:int}/submissions", async Task<Results<NotFound<string>, Ok<ICollection<ProblemSubmissionsDto>>>>
                 (ISubmissionService submissionService, int problemId) =>
        {
            var submissions = await submissionService.GetProblemSubmissionsAsync(problemId);
            
            if(submissions is null)
            {
                return TypedResults.NotFound("There are no Submissions yet!");
            }

            return TypedResults.Ok(submissions);
        });
    }
}