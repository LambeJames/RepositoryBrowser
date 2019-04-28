using RepositoryBrowser.Interfaces.Persistence;
using RepositoryBrowser.Interfaces.Services;
using RepositoryBrowser.Interfaces.Services.Caching;
using RepositoryBrowser.Interfaces.Services.Logging;
using RepositoryBrowser.Services.Helpers;
using RepositoryBrowser.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryBrowser.Services
{
    public class UserService : IUserService
    {
        private readonly IUserPersistence _userPersistence;
        private readonly IRepositoryPersistence _repositoryPersistence;
        private readonly IUserCache _userCache;
        private readonly ILogger _logger;

        public UserService(IUserPersistence userPersistence, 
            IRepositoryPersistence repositoryPersistence,
            IUserCache userCache,
            ILogger logger)
        {
            _userPersistence = userPersistence;
            _repositoryPersistence = repositoryPersistence;
            _userCache = userCache;
            _logger = logger;
        }

        public async Task<UserViewModel> Get(string name)
        {
            var result = _userCache.Get(name);

            if (result == null)
            {
                var user = await _userPersistence.Get(name);

                if (user != null)
                {
                    result = UserMapper.ToViewModel(user);

                    var repositories = await _repositoryPersistence.Get(name);
                    var topRepositories = repositories?.OrderByDescending(repo => repo.StargazersCount).Take(5);

                    if (topRepositories != null && topRepositories.Any())
                        result.Repositories = topRepositories.Select(RepositoryMapper.ToViewModel);

                    _userCache.Put(result, name);

                    _logger.LogMessage($"Found user {name}");
                }
                else
                {
                    _logger.LogMessage($"Unable to find user {name}");
                }
            }

            return result;
        }
    }
}
