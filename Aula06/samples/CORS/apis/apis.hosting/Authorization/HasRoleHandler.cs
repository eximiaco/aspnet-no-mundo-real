using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace apis.hosting.Authorization
{
    public class HasRoleHandler : AuthorizationHandler<HasRoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasRoleRequirement requirement)
        {
            // If user does not have the scope claim, get out of here
            if (!context.User.HasClaim(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" 
                                            && c.Issuer == requirement.Issuer))
                return Task.CompletedTask;

            // Split the scopes string into an array
            var scopes = context.User
                .FindAll(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" 
                                && c.Issuer == requirement.Issuer)
                .Select(c=> c.Value.Split(' ')[0]);

            // Succeed if the scope array contains the required scope
            if (scopes.Any(s => s == requirement.Scope))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}