using RepositoryBrowser.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryBrowser.Interfaces.Persistence
{
    public interface IRepositoryPersistence
    {
        Task<IEnumerable<Repository>> Get(string name);
    }
}
