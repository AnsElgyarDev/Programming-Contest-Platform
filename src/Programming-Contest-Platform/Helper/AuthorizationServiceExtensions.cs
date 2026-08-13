using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Programming_Contest_Platform.Helper;

public static class AuthorizationServiceExtensions
{
    public static IServiceCollection AddAppPolicies(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy(AppPolicies.AdminOnly, policy => policy.RequireRole("Admin"))
            .AddPolicy(AppPolicies.UserOnly, policy => policy.RequireRole("User"));

        return services;
    }
}