using System.Runtime.Caching;
using RepositoryBrowser.Interfaces.Persistence;
using RepositoryBrowser.Interfaces.Persistence.Clients;
using RepositoryBrowser.Interfaces.Services;
using RepositoryBrowser.Interfaces.Services.Caching;
using RepositoryBrowser.Interfaces.Services.Caching.Clients;
using RepositoryBrowser.Persistence;
using RepositoryBrowser.Persistence.Clients;
using RepositoryBrowser.Services;
using RepositoryBrowser.Services.Caching;
using RepositoryBrowser.Services.Caching.Clients;
using SimpleInjector;
using System;
using RepositoryBrowser.Interfaces.Services.Logging;
using RepositoryBrowser.Services.Logging;
using RepositoryBrowser.Models;

namespace RepositoryBrowser.IoC
{
    public class Dependencies
    {
        public void Register(Container container, Settings settings)
        {
            RegisterPersistence(container, settings);
            RegisterServices(container);
            RegisterCaching(container);
            RegisterLogging(container);
        }

        void RegisterServices(Container container)
        {
            container.Register<IUserService, UserService>(Lifestyle.Scoped);
        }

        void RegisterPersistence(Container container, Settings settings)
        {
            container.RegisterSingleton<IGitHubHttpClient>(() => new GitHubHttpClient(new System.Net.Http.HttpClient(), settings.GitHubBaseAddress));

            container.Register<IUserPersistence, UserPersistence>(Lifestyle.Scoped);
            container.Register<IRepositoryPersistence, RepositoryPersistence>(Lifestyle.Scoped);
        }

        void RegisterCaching(Container container)
        {
            container.RegisterSingleton<ILocalCacheClient>(() => new LocalCacheClient(MemoryCache.Default));
            container.Register<IUserCache, UserCache>(Lifestyle.Scoped);
        }

        void RegisterLogging(Container container)
        {
            container.Register<ILogger, Logger>(Lifestyle.Scoped);
        }
    }
}
