using System;

namespace TidalExplorer.TidalIntegration.MoveToCommonLibrary
{
    public static class GenericDeserializer
    {
        public static T Deserialize<T>(Func<string> getSerializedValue, Func<string, T> deserializeIt, bool throwExceptions = false)
        {
            string serialized;
            try
            {
                serialized = getSerializedValue();
            }
            catch (Exception)
            {
                if (throwExceptions)
                    throw;
                return default(T);
            }
            if (serialized == null)
                return default(T);
            T deserialized;
            try
            {
                deserialized = deserializeIt(serialized);
            }
            catch (Exception)
            {
                if (throwExceptions)
                    throw;
                return default(T);
            }
            return deserialized;
        }
    }
}