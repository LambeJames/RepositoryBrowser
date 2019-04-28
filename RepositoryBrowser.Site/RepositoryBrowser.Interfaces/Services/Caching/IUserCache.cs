using RepositoryBrowser.ViewModels;

namespace RepositoryBrowser.Interfaces.Services.Caching
{
    public interface IUserCache
    {
        void Put(UserViewModel user, string name);
        UserViewModel Get(string name);
    }
}
