using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Sum.Api.ServiceExtension
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var claim = principal.FindFirst(ClaimTypes.Sid);
            return claim?.Value;
        }

        public static string GetUserName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var claim = principal.FindFirst(ClaimTypes.Name);
            return claim?.Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var claim = principal.FindFirst(ClaimTypes.Email);
            return claim?.Value;
        }

        public static string GetUserRole(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var claim = principal.FindFirst(ClaimTypes.Role);
            return claim?.Value;
        }
    }
}