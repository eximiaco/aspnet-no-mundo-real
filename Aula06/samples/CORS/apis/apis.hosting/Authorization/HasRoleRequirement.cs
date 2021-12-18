using System;
using Microsoft.AspNetCore.Authorization;

namespace apis.hosting.Authorization
{
    public class HasRoleRequirement: IAuthorizationRequirement
    {
        public string Issuer { get; }
        public string Scope { get; }

        public HasRoleRequirement(string scope, string issuer)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
        }
    }
}