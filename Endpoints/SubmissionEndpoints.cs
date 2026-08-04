using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Helper;
using Programming_Contest_Platform.Services;

namespace Programming_Contest_Platform.Endpoints;

public static class SubmissionEndpoints
{
    public static async Task UseSubmissionEndpoints(this WebApplication app)
    {
        app.MapGet("/api/users/me/recentSubmissions", async Task<Results<NotFound<string>, Ok<ICollection<SubmissionSummaryDto>>>> 
            (ISubmissionService submissionService, ClaimsPrincipal user) =>
        {
            var userIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userIdClaim is null || !Guid.TryParse(userIdClaim, out Guid userId))
            {
                return TypedResults.NotFound("Invalid or missing user ID in token.");
            }

            var recentUserSubmissions = await submissionService.GetUserRecentSubmissionsAsync(userId);
            
            if (recentUserSubmissions is null || !recentUserSubmissions.Any())
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
        
        app.MapPost("api/problems/{problemId:int}/submissions", async 
                   (ISubmissionService submissionService, ProblemSubmissionDto problemSubmissionDto, int problemId) =>
        {
            problemSubmissionDto.problemId = problemId; 

                var result = await submissionService.SubmitProblem(problemSubmissionDto);
                
                if (result.isFailure)
                {
                    return Results.BadRequest(result.ErrorMessage);
                }

                return Results.Created($"/api/submissions/{result.Data}", new { Submissionstatus= result.Data });
        });

        app.MapGet("api/problems/supported-languages", () =>
        {
            var languages = SupportedLanguages.All;
            return TypedResults.Ok(languages);
        });
    }
}