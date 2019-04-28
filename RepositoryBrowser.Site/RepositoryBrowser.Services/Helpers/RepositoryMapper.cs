using RepositoryBrowser.Models;
using RepositoryBrowser.ViewModels;

namespace RepositoryBrowser.Services.Helpers
{
    public static class RepositoryMapper
    {
        public static RepositoryViewModel ToViewModel(Repository repository)
        {
            return new RepositoryViewModel
            {
                Name = repository.Name,
                Description = repository.Description,
                StargazersCount = repository.StargazersCount
            };
        }
    }
}
