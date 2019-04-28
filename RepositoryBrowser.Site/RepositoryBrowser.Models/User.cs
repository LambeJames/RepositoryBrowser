using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RepositoryBrowser.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Location { get; set; }
        [JsonProperty("Avatar_url")]
        public string AvatarUrl { get; set; }
    }
}
