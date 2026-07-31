using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http.HttpResults;
using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Entity;
using Programming_Contest_Platform.Services;

namespace Programming_Contest_Platform.Endpoints;

public static class AdminEndpoints
{
    public static async Task UseAdminEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/admin/").WithTags("Users Authentication");

        group.MapPost("", async Task<Results<BadRequest<string>, Created<ProblemSummaryDto>>>
                 (IAdminService adminService, Problem problem) =>
        {
            var result = await adminService.CreateProblem(problem);
            
            if(result.isFailure)
            {
                return TypedResults.BadRequest(result.ErrorMessage);
            }

            var problemToReturn = new ProblemSummaryDto
            {
                ProblemName = problem.ProblemName,
                ProblemLevel = problem.ProblemLevel
            };

            return TypedResults.Created("/api/admin/create", problemToReturn);
        });

        group.MapPut("{ProblemId}",async Task<Results<BadRequest<string>, Ok<ProblemSummaryDto>>>
                  (IAdminService adminService, int ProblemId, ProblemDetailsDto problemDetailsDto) =>
        {
            var result = await adminService.UpdateProblem(ProblemId, problemDetailsDto);
            
            if(result.isFailure)
            {
                return TypedResults.BadRequest(result.ErrorMessage);
            }

            var problemToReturn = new ProblemSummaryDto
            {
                ProblemName = problemDetailsDto.ProblemName, 
                ProblemLevel = problemDetailsDto.ProblemLevel   
            };

            return TypedResults.Ok(problemToReturn);
        });

        group.MapDelete("{ProblemId}", async Task<Results<BadRequest<string>, Ok<string>>>
                     (int ProblemId, IAdminService adminService) =>
        {
            var result = await adminService.RemoveProblem(ProblemId);
            
            if(result.isFailure)
            {
                return TypedResults.BadRequest(result.ErrorMessage);   
            }

            return TypedResults.Ok("Problem Deleted Successfuly!"); 
        });

    }
}