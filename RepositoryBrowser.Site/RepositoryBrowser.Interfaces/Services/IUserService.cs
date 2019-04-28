using RepositoryBrowser.ViewModels;
using System.Threading.Tasks;

namespace RepositoryBrowser.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserViewModel> Get(string name);
    }
}
