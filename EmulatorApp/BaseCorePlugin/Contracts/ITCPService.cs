using BaseCorePlugin.Model;
using static BaseCorePlugin.Services.TCPService;

namespace BaseCorePlugin.Contracts
{
    public interface ITCPService
    {
        ResultMapModel Create(string ipAddress, int port);
        ResultMapModel StartReceive(PushHandler callBack);
        ResultMapModel StopReceive();
        ResultMapModel Send(string data);
    }
}
