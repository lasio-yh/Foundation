using System.Management;

namespace BaseCorePlugin.Contracts
{
    public interface IDeviceService
    {
        ManagementObjectCollection GetUsbInfo();
        void GetDeviceInfoList();
        void GetScreenCopy(string fileName);
        void SetBrightness(byte targetBrightness);
        void GetPrintList(string printerName);
        bool IsInternetConnected();
        string GetIpAddress();
    }
}
