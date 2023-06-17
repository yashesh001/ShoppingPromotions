using app.shoppingpromotions.infrastructure.Interfaces;
using Enyim.Caching;
using Enyim.Caching.Memcached;

namespace app.shoppingpromotions.infrastructure
{
    public class MemcachedCacheWrapper : ICacheWrapper
    {
        private readonly IMemcachedClient _cache;

        public MemcachedCacheWrapper(IMemcachedClient cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public void Set<T>(string key, T value, TimeSpan expiration)
        {
            _cache.Store(StoreMode.Set, key, value, expiration);
        }

        public bool Remove(string key)
        {
            return _cache.Remove(key);
        }
    }
}
