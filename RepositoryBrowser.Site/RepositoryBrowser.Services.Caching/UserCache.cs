using RepositoryBrowser.Interfaces.Services.Caching;
using RepositoryBrowser.Interfaces.Services.Caching.Clients;
using RepositoryBrowser.ViewModels;

namespace RepositoryBrowser.Services.Caching
{
    public class UserCache : IUserCache
    {
        private readonly ILocalCacheClient _localCacheClient;

        public UserCache(ILocalCacheClient localCacheClient)
        {
            _localCacheClient = localCacheClient;
        }

        public void Put(UserViewModel user, string name)
        {
            _localCacheClient.Put(name, user);
        }

        public UserViewModel Get(string name)
        {
            return _localCacheClient.Get<UserViewModel>(name);
        }
    }
}
