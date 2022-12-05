using BaseCorePlugin.Model;
using System.Threading.Tasks;

namespace BaseCorePlugin.Contracts
{
    public interface ISOAPService
    {
        string Data { get; set; }
        string Uri { get; set; }
        string Token { get; set; }
        string Header { get; set; }
        string Action { get; set; }
        ResultMapModel Create();
        Task<string> SendAsync();
    }
}
