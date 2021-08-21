using Newtonsoft.Json;

namespace Core.Extensions
{
    public static class CloneServiceExtensions
    {
        public static T Clone<T>(this T source)
        {
            if (ReferenceEquals(source, null))
            {
                return default;
            }

            var deserializeSettings = new JsonSerializerSettings
                { ObjectCreationHandling = ObjectCreationHandling.Replace };
            var serializeSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source, serializeSettings), deserializeSettings);
        }
    }
}