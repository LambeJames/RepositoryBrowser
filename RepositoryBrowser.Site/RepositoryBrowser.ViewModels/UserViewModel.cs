using RepositoryBrowser.Models;
using System;
using System.Collections.Generic;

namespace RepositoryBrowser.ViewModels
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Avatar { get; set; }
        public IEnumerable<RepositoryViewModel> Repositories { get; set; }
    }
}
