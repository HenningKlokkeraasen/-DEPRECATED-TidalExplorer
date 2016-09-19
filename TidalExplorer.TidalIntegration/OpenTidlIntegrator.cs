using System.Security.Principal;
using OpenTidl;
using OpenTidl.Methods;
using TidalExplorer.TidalIntegration.AspNetIdentity;

namespace TidalExplorer.TidalIntegration
{
    public static class OpenTidlIntegrator
    {
        private static OpenTidlClient _client;
        public static OpenTidlClient Client => _client ?? (_client = new OpenTidlClient(ClientConfiguration.Default));

        public static OpenTidlSession BuildSession(IIdentity identity)
        {
            var tidalLoginModel = TidalClaimsDeserializer.DeserializeLoginModel(identity);
            return new OpenTidlSession(Client, tidalLoginModel);
        }
    }
}