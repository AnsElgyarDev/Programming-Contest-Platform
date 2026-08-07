using Microsoft.AspNetCore.Http.HttpResults;
using Programming_Contest_Platform.DTO;
using Programming_Contest_Platform.Entity;
using Programming_Contest_Platform.Helper;
using Programming_Contest_Platform.Services;

namespace Programming_Contest_Platform.Endpoints;

public static class AdminEndpoints
{
    public static WebApplication MapAdminEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/admin/problems").WithTags("Admin Problems Management");

        group.MapPost("", async Task<Results<BadRequest<string>, Created<ProblemSummaryDto>>>
            (IAdminService adminService, Problem problem) =>
        {
            var result = await adminService.CreateProblem(problem);

            if (result.isFailure)
            {
                return TypedResults.BadRequest(result.ErrorMessage);
            }

            var problemToReturn = new ProblemSummaryDto
            {
                ProblemName = problem.ProblemName,
                ProblemLevel = problem.ProblemLevel
            };

            return TypedResults.Created($"/api/admin/problems/{problem.ProblemId}", problemToReturn);
        }).RequireAuthorization(AppPolicies.AdminOnly);

        group.MapPut("{problemId:int}", async Task<Results<BadRequest<string>, NotFound<string>, Ok<ProblemSummaryDto>>>
            (int problemId, ProblemDetailsDto problemDetailsDto, IAdminService adminService) =>
        {
            var result = await adminService.UpdateProblem(problemId, problemDetailsDto);

            if (result.isFailure)
            {
                if (result.ErrorMessage.Contains("not found", StringComparison.OrdinalIgnoreCase))
                {
                    return TypedResults.NotFound(result.ErrorMessage);
                }

                return TypedResults.BadRequest(result.ErrorMessage);
            }

            var problemToReturn = new ProblemSummaryDto
            {
                ProblemName = problemDetailsDto.ProblemName,
                ProblemLevel = problemDetailsDto.ProblemLevel
            };

            return TypedResults.Ok(problemToReturn);
        }).RequireAuthorization(AppPolicies.AdminOnly);

        group.MapDelete("{problemId:int}", async Task<Results<NotFound<string>, NoContent>>
            (int problemId, IAdminService adminService) =>
        {
            var result = await adminService.RemoveProblem(problemId);

            if (result.isFailure)
            {
                return TypedResults.NotFound(result.ErrorMessage);
            }

            return TypedResults.NoContent();
        }).RequireAuthorization(AppPolicies.AdminOnly);

        return app;
    }
}