using Newtonsoft.Json;
using StackExchange.Redis;

namespace IntelligentHack.IntelligentCache
{
    /// <summary>
    /// An implementation of <see cref="IRedisSerializer" /> that encodes objects as JSON.
    /// </summary>
    public class JsonStringSerializer : IRedisSerializer
    {
        public T Deserialize<T>(RedisValue value)
        {
            return JsonConvert.DeserializeObject<T>(value, _serializerSettings);
        }

        public RedisValue Serialize<T>(T instance)
        {
            return JsonConvert.SerializeObject(instance, typeof(T), _serializerSettings);
        }

        private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        };
    }
}
