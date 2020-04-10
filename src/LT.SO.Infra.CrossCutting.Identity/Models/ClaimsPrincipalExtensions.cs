using System;
using System.Security.Claims;

namespace LT.SO.Infra.CrossCutting.Identity.Models
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentException(nameof(principal));

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);

            return claim?.Value;
        }
    }
}