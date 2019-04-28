using RepositoryBrowser.Interfaces.Persistence.Clients;
using System;
using System.Net.Http;

namespace RepositoryBrowser.Persistence.Clients
{
    public class GitHubHttpClient : HttpClientWrapper, IGitHubHttpClient
    {
        public GitHubHttpClient(HttpClient httpClient, string baseAddresss) : base(httpClient)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "http://api.github.com/v3/#user-agent-required");
            httpClient.BaseAddress = new Uri(baseAddresss);
        }
    }
}
