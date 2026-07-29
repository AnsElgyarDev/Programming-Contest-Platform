using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Helper.ClaimsPrincipalExtensions;
using Programming_Contest_Platform.Services;

namespace Programming_Contest_Platform.Endpoints;

public static class ProblemEndpoints
{
    public static async Task UseProblemEndpoints(this WebApplication app)
    {
        
        var group = app.MapGroup("api/problems").WithTags("Users Authentication");
        
        
        group.MapGet("", async Task<Results<NotFound<string>, Ok<ICollection<ProblemSummaryDto>>>>
        (IProblemService problemService) =>
        {
            var problems = await problemService.GetAllProblems();
            
            if(problems is null)
                return TypedResults.NotFound("There are No Problems At this Moment!");
            
            return TypedResults.Ok(problems);
        });
    }
}