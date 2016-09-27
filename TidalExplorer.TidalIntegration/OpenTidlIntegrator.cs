using System.Security.Principal;
using OpenTidl;
using OpenTidl.Methods;
using TidalExplorer.TidalIntegration.AspNetIdentity;
using System.Threading.Tasks;

namespace TidalExplorer.TidalIntegration
{
    public static class OpenTidlIntegrator
    {
        private static OpenTidlClient _client;
        public static OpenTidlClient Client => _client ?? (_client = new OpenTidlClient(ClientConfiguration.Default));

        public static async Task<OpenTidlSession> RestoreSessionFromClaimsIdentity(IIdentity identity)
        {
            var tidalLoginModel = TidalClaimsDeserializer.DeserializeLoginModel(identity);
            return await Client.RestoreSession(tidalLoginModel.SessionId);
        }
    }
}