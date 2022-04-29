using Microsoft.AspNetCore.Authorization;
using ReleaseManager.Model.Enums;

namespace ReleaseManager.Api.Policies.Requirements;

public class RoleRequirement : IAuthorizationRequirement
{
    public Roles Role { get; }

    public RoleRequirement(Roles role) => Role = role;
}