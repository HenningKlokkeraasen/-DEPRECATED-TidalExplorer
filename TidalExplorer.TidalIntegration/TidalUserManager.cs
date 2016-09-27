using System;
using System.Security.Principal;
using System.Threading.Tasks;
using OpenTidl.Methods;

namespace TidalExplorer.TidalIntegration
{
    public class TidalUserManager
    {
        public static async Task<OpenTidlSession> LoginToTidal(string username, string password)
        {
            try
            {
                return await OpenTidlIntegrator.Client.LoginWithUsername(username, password);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task LogOutFromTidal(IIdentity identity)
        {
            var session = await OpenTidlIntegrator.RestoreSessionFromClaimsIdentity(identity);
            await session.Logout();
        }
    }
}