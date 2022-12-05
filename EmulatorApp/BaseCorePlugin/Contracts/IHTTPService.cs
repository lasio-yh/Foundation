using BaseCorePlugin.Model;
using System.Threading.Tasks;

namespace BaseCorePlugin.Contracts
{
    public interface IHTTPService
    {
        Task<ResultMapModel> GetRequest(string uri, string token);
        Task<ResultMapModel> PostRequest(string uri, string body, string token);
    }
}
