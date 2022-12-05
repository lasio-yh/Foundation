using BaseCorePlugin.Contracts;
using BaseCorePlugin.Model;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

namespace BaseCorePlugin.Services
{
    public class UDPService : IUDPService
    {
        public delegate void PushHandler(object args);
        public PushHandler _push;
        private UdpClient _udpClient;
        int _port;
        Thread _worker;

        /// <summary>
        /// UDP Client 를 생성합니다.
        /// </summary>
        /// <returns>ResultMapModel</returns>
        public ResultMapModel Create()
        {
            try
            {
                var endPoint = new IPEndPoint(IPAddress.Any, _port);
                _udpClient = new UdpClient(endPoint);
                return new ResultMapModel { ResultId = "0x00", ResultMessage = "Succes" };
            }
            catch (Exception ex)
            {
                return new ResultMapModel { ResultId = "0x01", ResultMessage = ex.Message };
            }
        }

        /// <summary>
        /// 데이터 수신을 시작합니다.
        /// </summary>
        /// <param name="port"></param>
        /// <returns>ResultMapModel</returns>
        public ResultMapModel StartReceive(int port, PushHandler callBack)
        {
            try
            {
                _worker = new Thread(new ThreadStart(Receive));
                _port = port;
                _worker.Start();
                _push = callBack;

                return new ResultMapModel { ResultId = "0x00", ResultMessage = "Succes" };
            }
            catch (Exception ex)
            {
                return new ResultMapModel { ResultId = "0x01", ResultMessage = ex.Message };
            }
        }

        /// <summary>
        /// 데이터 수신을 중지합니다.
        /// </summary>
        /// <returns>ResultMapModel</returns>
        public ResultMapModel StopReceive()
        {
            try
            {
                if (_worker != null)
                {
                    _worker.Abort();
                    _worker = null;
                }

                if (_udpClient != null)
                {
                    _udpClient.Close();
                    _udpClient = null;
                }

                return new ResultMapModel { ResultId = "0x00", ResultMessage = "Succes" };
            }
            catch (Exception ex)
            {
                return new ResultMapModel { ResultId = "0x01", ResultMessage = ex.Message };
            }
        }

        /// <summary>
        /// 데이터를 전송합니다.
        /// </summary>
        /// <returns>ResultMapModel</returns>
        public ResultMapModel Send(byte[] buffer, int length)
        {
            try
            {
                _udpClient.Send(buffer, length);
                return new ResultMapModel { ResultId = "0x00", ResultMessage = "Succes" };
            }
            catch (Exception ex)
            {
                return new ResultMapModel { ResultId = "0x01", ResultMessage = ex.Message };
            }
        }

        private void Receive()
        {
            try
            {
                var remoteEndPoint = new IPEndPoint(IPAddress.Any, _port);
                while (_udpClient.Client.Connected) 
                {
                    try
                    {
                        if (_udpClient == null)
                            Create();

                        var data = _udpClient.Receive(ref remoteEndPoint);

                        //data
                        _push(new object());
                        Thread.Sleep(100);
                    }
                    catch (Exception ex)
                    {
                        Thread.Sleep(1000);
                    }
                }
            }
            catch (Exception ex)
            {
                Thread.Sleep(1000);
            }
        }

        public byte[] StructureToByte(object obj)
        {
            int datasize = Marshal.SizeOf(obj);                 // 구조체에 할당된 메모리의 크기를 구한다.
            IntPtr buff = Marshal.AllocHGlobal(datasize);       // 비관리 메모리 영역에 구조체 크기만큼의 메모리를 할당한다.
            Marshal.StructureToPtr(obj, buff, false);           // 할당된 구조체 객체의 주소를 구한다.
            byte[] data = new byte[datasize];                   // 구조체가 복사될 배열
            Marshal.Copy(buff, data, 0, datasize);              // 구조체 객체를 배열에 복사
            Marshal.FreeHGlobal(buff);                          // 비관리 메모리 영역에 할당했던 메모리를 해제함
            return data;                                        // 배열을 리턴
        }

        public object ByteToStructure(byte[] data, Type type)
        {
            IntPtr buff = Marshal.AllocHGlobal(data.Length);    // 배열의 크기만큼 비관리 메모리 영역에 메모리를 할당한다.
            Marshal.Copy(data, 0, buff, data.Length);           // 배열에 저장된 데이터를 위에서 할당한 메모리 영역에 복사한다.
            object obj = Marshal.PtrToStructure(buff, type);    // 복사된 데이터를 구조체 객체로 변환한다.
            Marshal.FreeHGlobal(buff);                          // 비관리 메모리 영역에 할당했던 메모리를 해제함
            if (Marshal.SizeOf(obj) != data.Length)             // 구조체와 원래의 데이터의 크기 비교
            {
                return null;                                    // 크기가 다르면 null 리턴
            }
            return obj;                                         // 구조체 리턴
        }
    }
}