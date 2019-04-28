using RepositoryBrowser.Interfaces.Services.Caching.Clients;
using System;
using System.Runtime.Caching;

namespace RepositoryBrowser.Services.Caching.Clients
{
    public class LocalCacheClient : ILocalCacheClient
    {
        ObjectCache _cache { get; set; }
        readonly TimeSpan _defaultTTL = TimeSpan.FromMinutes(1);

        public LocalCacheClient(ObjectCache memoryCache)
        {
            _cache = memoryCache;
        }

        public T Get<T>(string key, bool useColdStorage = true) where T : class
        {
            return (T)_cache.Get(key);
        }

        public void Put<T>(string key, T value, TimeSpan? timeToLive = null) where T : class
        {
            CacheItemPolicy policy = new CacheItemPolicy();

            if (timeToLive.HasValue)
                policy.SlidingExpiration = timeToLive.Value;
            else
                policy.SlidingExpiration = _defaultTTL;

            _cache.Set(key, value, policy);
        }
    }
}
