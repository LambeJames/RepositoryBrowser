using RepositoryBrowser.Interfaces.Persistence;
using RepositoryBrowser.Interfaces.Services;
using RepositoryBrowser.Interfaces.Services.Caching;
using RepositoryBrowser.Interfaces.Services.Logging;
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
                    var repositories = await _repositoryPersistence.Get(name);
                    var topRepositories = repositories.OrderByDescending(repo => repo.StargazersCount).Take(5);

                    result = new UserViewModel
                    {
                        Name = user.Name,
                        Location = user.Location,
                        Avatar = user.AvatarUrl,
                        Repositories = topRepositories.Select(repo => new RepositoryViewModel
                        {
                            Name = repo.Name,
                            Description = repo.Description,
                            StargazersCount = repo.StargazersCount
                        })
                    };

                    _userCache.Put(result, name);
                }
            }

            _logger.LogMessage($"Found user {name}");

            return result;
        }
    }
}
