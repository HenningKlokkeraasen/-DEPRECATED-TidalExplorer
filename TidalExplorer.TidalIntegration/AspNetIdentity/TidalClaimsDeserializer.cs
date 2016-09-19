using System;
using System.Security.Principal;
using Newtonsoft.Json;
using OpenTidl.Models;
using TidalExplorer.TidalIntegration.MoveToCommonLibrary;

namespace TidalExplorer.TidalIntegration.AspNetIdentity
{
    public static class TidalClaimsDeserializer
    {
        public static UserModel DeserializeUserModel(IIdentity identity)
        {
            return Deserialize<UserModel>(identity.GetTidalUserModelSerialized);
        }

        public static LoginModel DeserializeLoginModel(IIdentity identity)
        {
            return Deserialize<LoginModel>(identity.GetTidalLoginModelSerialized);
        }

        private static T Deserialize<T>(Func<string> func)
        {
            return GenericDeserializer.Deserialize(func, JsonConvert.DeserializeObject<T>);
        }
    }
}