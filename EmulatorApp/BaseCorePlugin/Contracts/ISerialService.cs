using BaseCorePlugin.Model;
using System.IO.Ports;
using static BaseCorePlugin.Services.SerialService;

namespace BaseCorePlugin.Contracts
{
    public interface ISerialService
    {
        SerialPort Client { get; }
        string PortName { get; set; }
        int BaudRate { get; set; }
        int DataBits { get; set; }
        Parity Parity { get; set; }
        StopBits StopBits { get; set; }
        bool CDHolding { get;  }
        bool CtsHolding { get; }
        bool DsrHolding { get; }
        bool DtrEnable { get; }
        bool IsOpen { get; }

        ResultMapModel Create(string portName, PushHandler callBack);
        ResultMapModel Open();
        ResultMapModel Pause();
        ResultMapModel Resume();
        ResultMapModel Close();
        ResultMapModel WriteLine(string text);
        ResultMapModel Write(byte[] buffer, int offset, int count);
    }
}
