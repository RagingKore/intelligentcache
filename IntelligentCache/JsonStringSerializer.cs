using Newtonsoft.Json;
using StackExchange.Redis;

namespace IntelligentHack.IntelligentCache
{
    public class JsonStringSerializer : IRedisSerializer
    {
        public T Deserialize<T>(RedisValue value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public RedisValue Serialize<T>(T instance)
        {
            return JsonConvert.SerializeObject(instance);
        }
    }
}
