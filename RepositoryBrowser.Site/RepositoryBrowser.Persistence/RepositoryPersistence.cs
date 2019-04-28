using RepositoryBrowser.Interfaces.Persistence;
using RepositoryBrowser.Interfaces.Persistence.Clients;
using RepositoryBrowser.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryBrowser.Persistence
{
    public class RepositoryPersistence : IRepositoryPersistence
    {
        private readonly IGitHubHttpClient _httpClient;

        public RepositoryPersistence(IGitHubHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Repository>> Get(string name)
        {
            return await _httpClient.Get<IEnumerable<Repository>>($"users/{name}/repos");
        }
    }
}
