using RepositoryBrowser.Models;
using System.Threading.Tasks;

namespace RepositoryBrowser.Interfaces.Persistence
{
    public interface IUserPersistence
    {
        Task<User> Get(string name);
    }
}
