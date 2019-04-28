using System.Threading.Tasks;

namespace RepositoryBrowser.Interfaces.Persistence.Clients
{
    public interface IHttpClientWrapper
    {
        Task<T> Get<T>(string route);
    }
}
