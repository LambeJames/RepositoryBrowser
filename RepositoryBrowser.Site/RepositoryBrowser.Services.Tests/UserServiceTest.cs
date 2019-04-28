using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using RepositoryBrowser.Interfaces.Persistence;
using RepositoryBrowser.Interfaces.Services.Caching;
using RepositoryBrowser.Interfaces.Services.Logging;
using RepositoryBrowser.Models;

namespace RepositoryBrowser.Services.Tests
{
    [TestFixture]
    public class UserServiceTest
    {
        private readonly IUserPersistence _userPersistence;
        private readonly IRepositoryPersistence _repositoryPersistence;
        private readonly IUserCache _userCache;
        private readonly ILogger _logger;

        public UserServiceTest()
        {
            _logger = new Mock<ILogger>().Object;
            _userCache = new Mock<IUserCache>().Object;

            var mockedUserPersistence = new Mock<IUserPersistence>();

            mockedUserPersistence.Setup(m => m.Get(""))
                .Returns((string name) => Task.FromResult<User>(null));

            mockedUserPersistence.Setup(m => m.Get("invalidUser"))
               .Returns((string name) => Task.FromResult<User>(null));

            mockedUserPersistence.Setup(m => m.Get("validUser"))
               .Returns((string name) => Task.FromResult<User>(BuildValidUser()));

            mockedUserPersistence.Setup(m => m.Get("newUser"))
               .Returns((string name) => Task.FromResult<User>(BuildValidUser()));

            _userPersistence = mockedUserPersistence.Object;

            var mockedRepositoryPersistence = new Mock<IRepositoryPersistence>();

            mockedRepositoryPersistence.Setup(m => m.Get(""))
               .Returns((string name) => Task.FromResult<IEnumerable<Repository>>(null));

            mockedRepositoryPersistence.Setup(m => m.Get("invalidUser"))
               .Returns((string name) => Task.FromResult<IEnumerable<Repository>>(null));

            mockedRepositoryPersistence.Setup(m => m.Get("validUser"))
               .Returns((string name) => Task.FromResult<IEnumerable<Repository>>(BuildRepositories()));

            mockedRepositoryPersistence.Setup(m => m.Get("newUser"))
               .Returns((string name) => Task.FromResult<IEnumerable<Repository>>(new List<Repository>()));

            _repositoryPersistence = mockedRepositoryPersistence.Object;
        }

        private User BuildValidUser()
        {
            return new User
            {
                Name = "ValidUser",
                Location = "Here",
                AvatarUrl = "me.jpg"
            };
        }

        private IEnumerable<Repository> BuildRepositories()
        {
            int[] items = Enumerable.Range(1, 10).ToArray();

            return items.Select(item => BuildRepository(item));
        }

        private Repository BuildRepository(int stargazerCount)
        {
            return new Repository
            {
                Name = "MyRepo",
                Description = "Repo",
                StargazersCount = stargazerCount
            };
        }

        [Test]
        public async Task Get_EmptyName_NullReturned()
        {
            var userService = new UserService(_userPersistence, _repositoryPersistence, _userCache, _logger);
            var result = await userService.Get("");

            Assert.True(result == null);
        }

        [Test]
        public async Task Get_InValidUser_NullReturned()
        {
            var userService = new UserService(_userPersistence, _repositoryPersistence, _userCache, _logger);
            var result = await userService.Get("invalidUser");

            Assert.True(result == null);
        }

        [Test]
        public async Task Get_ValidUserNoRepositories_UserReturnedNoRepositories()
        {
            var userService = new UserService(_userPersistence, _repositoryPersistence, _userCache, _logger);
            var result = await userService.Get("newUser");

            Assert.True(result.Repositories.Count() == 0);
        }

        [Test]
        public async Task Get_ValidUser_UserFound()
        {
            var userService = new UserService(_userPersistence, _repositoryPersistence, _userCache, _logger);
            var result = await userService.Get("validUser");

            Assert.True(!string.IsNullOrEmpty(result.Name));
        }

        [Test]
        public async Task Get_ValidUser_UserFoundWithFiveRepositories()
        {
            var userService = new UserService(_userPersistence, _repositoryPersistence, _userCache, _logger);
            var result = await userService.Get("validUser");

            Assert.AreEqual(result.Repositories.Count(), 5);
        }

        [Test]
        public async Task Get_ValidUser_UserHsaTopFiveRepositories()
        {
            var userService = new UserService(_userPersistence, _repositoryPersistence, _userCache, _logger);
            var result = await userService.Get("validUser");

            Assert.AreEqual(result.Repositories.Where(repo => repo.StargazersCount > 5).Count(), 5);
        }
    }
}
