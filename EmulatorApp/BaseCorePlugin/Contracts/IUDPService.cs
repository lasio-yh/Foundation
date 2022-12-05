using BaseCorePlugin.Model;
using static BaseCorePlugin.Services.UDPService;

namespace BaseCorePlugin.Contracts
{
    public interface IUDPService
    {
        ResultMapModel Create();
        ResultMapModel StartReceive(int port, PushHandler callBack);
        ResultMapModel StopReceive();
        ResultMapModel Send(byte[] buffer, int length);
    }
}
