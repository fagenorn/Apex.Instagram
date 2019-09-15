using System.Text.Json;

namespace Apex.Instagram.API.Response.Serializer
{
    internal static class JsonSerializerDefaultOptions
    {
        #region Singleton     

        private static JsonSerializerOptions _instance;

        private static readonly object Lock = new object();

        public static JsonSerializerOptions Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ??= new JsonSerializerOptions
                                         {
                                             PropertyNameCaseInsensitive = true,
                                             PropertyNamingPolicy        = new JsonSnakeCaseNamingPolicy(),
                                             DictionaryKeyPolicy         = new JsonSnakeCaseNamingPolicy(),
                                             Converters =
                                             {
                                                 new DurableStringConverter(),
                                                 new StringToUlongConverter()
                                             }
                                         };
                }
            }
        }

        #endregion
    }
}