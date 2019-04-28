using RepositoryBrowser.Interfaces.Persistence;
using RepositoryBrowser.Interfaces.Persistence.Clients;
using RepositoryBrowser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryBrowser.Persistence
{
    public class UserPersistence : IUserPersistence
    {
        private readonly IGitHubHttpClient _httpClient;

        public UserPersistence(IGitHubHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<User> Get(string name)
        {
            return await _httpClient.Get<User>($"users/{name}");
        }
    }
}
