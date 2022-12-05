using BaseCorePlugin.Contracts;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows;
using System.Web.ModelBinding;

namespace BaseCorePlugin.Services
{
    public class DeviceService : IDeviceService
    {
        //TODO 시스템 장치관리자 모든 기능 포함하도록 함.

        public delegate void PushHandler(object args);
        private PushHandler _push;

        /// <summary>
        /// 네트워크 상태 확인
        /// </summary>
        public bool NetworkConnected => NetworkInterface.GetIsNetworkAvailable();

        /// <summary>
        /// 메모리 용량 확인
        /// </summary>
        public long CurrentMemory => GC.GetTotalMemory(false);

        public DeviceService(PushHandler callback)
        {
            if (callback == null)
                return;

            _push = callback;

            //네트워크 상태 변경 시 푸시
            NetworkChange.NetworkAvailabilityChanged += (sender, e) => _push(e.IsAvailable);
        }

        public ManagementObjectCollection GetUsbInfo()
        {
            //TODO USB 정보 반환
            //(string)device.GetPropertyValue("DeviceID")
            //(string)device.GetPropertyValue("PNPDeviceID")
            //(string)device.GetPropertyValue("Description")
            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_USBHub"))
            {
                collection = searcher.Get();
            }
            return collection;
        }

        public bool IsInternetConnected()
        {
            const string NCSI_TEST_URL = "http://www.msftncsi.com/ncsi.txt";
            const string NCSI_TEST_RESULT = "Microsoft NCSI";
            const string NCSI_DNS = "dns.msftncsi.com";
            const string NCSI_DNS_IP_ADDRESS = "131.107.255.255";
            try
            {
                // Check NCSI test link
                var webClient = new WebClient();
                string result = webClient.DownloadString(NCSI_TEST_URL);
                if (result != NCSI_TEST_RESULT)
                    return false;

                // Check NCSI DNS IP
                var dnsHost = Dns.GetHostEntry(NCSI_DNS);
                if (dnsHost.AddressList.Count() < 0 || dnsHost.AddressList[0].ToString() != NCSI_DNS_IP_ADDRESS)
                    return false;
            }
            catch (Exception ex) {  return false; }
            return true;
        }

        public string GetIpAddress()
        {
            var client = new WebClient();
            var result = client.DownloadString("http://checkip.dyndns.org");
            return result.Split(':')[1].Split('<')[0].Trim();
        }

        public void GetDeviceInfoList()
        {
            try
            {
                //var deviceList = new ManagementObjectSearcher("Select * from Win32_PnPEntity where Availability = 2");
                var deviceList = new ManagementObjectSearcher("Select * from Win32_PnPEntity");
                if (deviceList != null)
                {
                    foreach (var device in deviceList.Get())
                    {
                        var nameValue = device.GetPropertyValue("Name");
                        var name = "NoName".ToString();
                        if (nameValue != null)
                            name = nameValue.ToString();

                        var status = device.GetPropertyValue("Status").ToString();
                        var working = ((status == "OK") || (status == "Degraded") || (status == "Pred Fail"));
                        _push($"{working}|{name}|{status}");
                    }
                }
            }
            catch (Exception ex) { _push(ex.ToString()); }
        }

        public void GetScreenCopy(string fileName)
        {
            try
            {
                // 주화면의 크기 정보 읽기
                int width = (int)SystemParameters.PrimaryScreenWidth;
                int height = (int)SystemParameters.PrimaryScreenHeight;

                // 화면 크기만큼의 Bitmap 생성
                using (Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb))
                {
                    // Bitmap 이미지 변경을 위해 Graphics 객체 생성
                    using (Graphics gr = Graphics.FromImage(bmp))
                    {
                        // 화면을 그대로 카피해서 Bitmap 메모리에 저장
                        gr.CopyFromScreen(0, 0, 0, 0, bmp.Size);
                    }
                    // Bitmap 데이타를 파일로 저장
                    bmp.Save($"{fileName}.png", ImageFormat.Png);
                }
            }
            catch (Exception ex) { _push(ex.ToString()); }
        }

        public void SetBrightness(byte targetBrightness)
        {
            //TODO 모니터 밝기 조정
            try
            {
                var scope = new ManagementScope("root\\WMI");
                var query = new SelectQuery("WmiMonitorBrightnessMethods");
                using (var searcher = new ManagementObjectSearcher(scope, query))
                {
                    using (var objectCollection = searcher.Get())
                    {
                        foreach (ManagementObject mObj in objectCollection)
                        {
                            mObj.InvokeMethod("WmiSetBrightness", new Object[] { UInt32.MaxValue, targetBrightness });
                            _push(mObj);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex) { _push($"지원하지 않는 모니터 입니다."); }
        }

        public void GetPrintList(string printerName)
        {
            //TODO 프린터 목록 반환
            try
            {
                var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Printer " + $"WHERE Name LIKE '%{printerName}'");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    _push(queryObj["Name"]);
                    _push(queryObj["PrinterStatus"]);
                }
            }
            catch (Exception ex) { _push($"지원하지 않는 프린터 입니다."); }
        }
    }
}