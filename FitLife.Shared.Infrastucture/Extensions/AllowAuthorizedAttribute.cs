using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace FitLife.Shared.Infrastructure.Extensions
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AllowAuthorizedAttribute : AuthorizeAttribute
    {
        public AllowAuthorizedAttribute(params object[] roles)
        {
            if (roles.Any(r => r.GetType().BaseType != typeof(Enum)))
                throw new ArgumentException("roles");

            this.Roles = string.Join(",", roles.Select(r => Enum.GetName(r.GetType(), r)));
        }
    }
}
