using Newtonsoft.Json;

namespace RepositoryBrowser.Models
{
    public class Repository
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonProperty("Stargazers_count")]
        public int StargazersCount { get; set; }
    }
}
