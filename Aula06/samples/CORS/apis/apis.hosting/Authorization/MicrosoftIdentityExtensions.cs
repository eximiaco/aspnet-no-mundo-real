using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;

namespace apis.hosting.Authorization
{
    public static class MicrosoftIdentityExtensions
    {
        public static AuthorizationOptions AddPolicies(this AuthorizationOptions options,
            IConfigurationSection section)
        {
            var roles = section.GetMappingConfiguration("RoleMappings").ToList();
            var scopes = section.GetMappingConfiguration("ScopeMappings").ToList();
            var policyNames = roles.Select(r => r.Value)
                .Union(scopes.Select(s => s.Value))
                .SelectMany(p => p.Split(','));

            foreach (var policyName in policyNames)
            {
                var roleConfigurations = roles.GetPolicyConfigurations(policyName);
                var scopeConfigurations = scopes.GetPolicyConfigurations(policyName);

                options.AddPolicy(policyName, builder => builder.RequireAssertion(ctx =>
                {
                    if (roleConfigurations.Any(rc => ctx.User.IsInRole(rc.Key))) return true;
                    return scopeConfigurations.Any(sc => ctx.User.HasScope(sc.Key));
                }));
            }

            return options;
        }

        private static bool HasScope(this ClaimsPrincipal principal, string scope)
        {
            if (string.IsNullOrEmpty(scope)) return false;

            var claim = principal.Claims.FirstOrDefault(c => c.Type is ClaimConstants.Scope or ClaimConstants.Scp);
            return claim is not null && claim.Value.Split(' ').Any(s => string.Equals(s, scope));
        }

        private static IEnumerable<IConfigurationSection>
            GetMappingConfiguration(this IConfigurationSection section, string name) => section.GetChildren()
            .Where(s => string.Equals(s.Key, name)).SelectMany(s => s.GetChildren());

        private static IEnumerable<IConfigurationSection> GetPolicyConfigurations(
            this IEnumerable<IConfigurationSection> sections,
            string policyName) => sections.Where(r => r.Value.Split(',').Any(p => string.Equals(p, policyName)));
    }
}