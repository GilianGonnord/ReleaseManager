using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using ReleaseManager.Api.Policies.Requirements;
using ReleaseManager.Model.Enums;

namespace ReleaseManager.Api.Policies.Handers;

public class RoleHandler : AuthorizationHandler<RoleRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
    {
        var pendingRequirements = context.PendingRequirements.ToList();
        var userIdClaim = context.User.FindFirst(c => c.Type == "user_id");

        if (userIdClaim is null)
        {
            return;
        }

        var user = await FirebaseAuth.DefaultInstance.GetUserAsync(userIdClaim.Value);

        var userRole = (Int64)user.CustomClaims["role"];

        if (userRole < (Int64)requirement.Role)
        {
            context.Succeed(requirement);
        }

        return;
    }
}