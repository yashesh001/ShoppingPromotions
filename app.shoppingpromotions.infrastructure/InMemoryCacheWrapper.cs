using app.shoppingpromotions.infrastructure.Interfaces;

namespace app.shoppingpromotions.infrastructure
{
    public class InMemoryCacheWrapper : ICacheWrapper
    {
        private readonly IDictionary<string, object> _cache;

        public InMemoryCacheWrapper()
        {
            _cache = new Dictionary<string, object>();
        }

        public T Get<T>(string key)
        {
            if (_cache.ContainsKey(key))
            {
                return (T)_cache[key];
            }
            else
            {
                return default;
            }
        }

        public void Set<T>(string key, T value, TimeSpan expiration)
        {
            if (_cache.ContainsKey(key))
            {
                _cache[key] = value;
            }
            else
            {
                _cache.TryAdd(key, value);
            }
        }

        public bool Remove(string key)
        {
            return _cache.Remove(key);
        }
    }
}