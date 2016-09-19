using System.Security.Claims;
using System.Security.Principal;

namespace TidalExplorer.TidalIntegration.AspNetIdentity
{
    public static class TidalIdentityExtensions
    {
        public static string GetTidalLoginModelSerialized(this IIdentity identity)
        {
            return (identity as ClaimsIdentity)?.FirstOrNull(TidalClaimTypes.TidalLoginModelSerialized);
        }

        public static string GetTidalUserModelSerialized(this IIdentity identity)
        {
            return (identity as ClaimsIdentity)?.FirstOrNull(TidalClaimTypes.TidalUserModelSerialized);
        }
        
        private static string FirstOrNull(this ClaimsIdentity identity, string claimType)
        {
            return identity.FindFirst(claimType)?.Value;
        }
    }
}