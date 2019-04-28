using System;

namespace RepositoryBrowser.Interfaces.Services.Caching.Clients
{
    public interface ILocalCacheClient
    {
        T Get<T>(string key, bool useColdStorage = true) where T : class;
        void Put<T>(string key, T value, TimeSpan? timeToLive = null) where T : class;
    }
}
