using BaseCorePlugin.Contracts;
using BaseCorePlugin.Model;
using System;
using System.IO.Ports;

namespace BaseCorePlugin.Services
{
    public class SerialService : ISerialService
    {
        public delegate void PushHandler(object args);
        public PushHandler _push;
        bool _isRead = false;

        SerialPort _client;
        public SerialPort Client
        {
            get => _client;
        }

        string _portName = string.Empty;
        public string PortName
        {
            get => _portName;
            set => _portName = string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        int _baudRate = 9600;
        public int BaudRate
        {
            get => _baudRate;
            set => _baudRate = value;
        }

        int _dataBits = 8;
        public int DataBits
        {
            get => _dataBits;
            set => _dataBits = value;
        }

        Parity _parity = Parity.None;
        public Parity Parity
        {
            get => _parity;
            set => _parity = value;
        }

        StopBits _stopBits = StopBits.One;
        public StopBits StopBits
        {
            get => _stopBits;
            set => _stopBits = value;
        }

        public bool CDHolding
        {
            get => _client.CDHolding;
        }

        public bool CtsHolding
        {
            get => _client.CDHolding;
        }

        public bool DsrHolding
        {
            get => _client.DsrHolding;
        }

        public bool DtrEnable
        {
            get => _client.DtrEnable;
        }

        public bool IsOpen
        {
            get => _client.IsOpen;
        }

        /// <summary>
        /// 시리얼통신 컨텍스트를 생성합니다.
        /// </summary>
        /// <param name="portName">COM 포트</param>
        /// <returns>void</returns>
        public ResultMapModel Create(string portName, PushHandler callBack)
        {
            try
            {
                _portName = portName;
                _client = new SerialPort();
                _client.PortName = _portName;
                _client.BaudRate = 9600;
                _client.DataBits = 8;
                _client.Parity = Parity.None;
                _client.StopBits = StopBits.One;
                _client.DataReceived += OnDataReceived;
                _client.Disposed += OnDisposed;
                _client.ErrorReceived += OnErrorReceived;
                _client.PinChanged += OnPinChanged;
                _push = callBack;
                return new ResultMapModel { ResultId = "0x00", ResultMessage = "Succes" };
            }
            catch (Exception ex)
            {
                return new ResultMapModel { ResultId = "0x01", ResultMessage = ex.Message };
            }
        }

        /// <summary>
        /// 시리얼 통신을 시작합니다.
        /// </summary>
        /// <returns>ResultMapModel</returns>
        public ResultMapModel Open()
        {
            try
            {
                _client.Open();
                return new ResultMapModel { ResultId = "0x00", ResultMessage = "Succes" };
            }
            catch (Exception ex)
            {
                return new ResultMapModel { ResultId = "0x01", ResultMessage = ex.Message };
            }
        }

        /// <summary>
        /// 데이터 수신을 일시중지 합니다.
        /// </summary>
        /// <returns>ResultMapModel</returns>
        public ResultMapModel Pause()
        {
            try
            {
                _isRead = false;
                return new ResultMapModel { ResultId = "0x00", ResultMessage = "Succes" };
            }
            catch (Exception ex)
            {
                return new ResultMapModel { ResultId = "0x01", ResultMessage = ex.Message };
            }
        }

        /// <summary>
        /// 데이터 수신을 다시 시작합니다.
        /// </summary>
        /// <returns>ResultMapModel</returns>
        public ResultMapModel Resume()
        {
            try
            {
                _isRead = true;
                return new ResultMapModel { ResultId = "0x00", ResultMessage = "Succes" };
            }
            catch (Exception ex)
            {
                return new ResultMapModel { ResultId = "0x01", ResultMessage = ex.Message };
            }
        }

        /// <summary>
        /// 시리얼통신을 종료합니다.
        /// </summary>
        /// <returns>ResultMapModel</returns>
        public ResultMapModel Close()
        {
            try
            {
                _client.Close();
                return new ResultMapModel { ResultId = "0x00", ResultMessage = "Succes" };
            }
            catch (Exception ex)
            {
                return new ResultMapModel { ResultId = "0x01", ResultMessage = ex.Message };
            }
        }

        /// <summary>
        /// 텍스트 형식의 데이터를 전송합니다.
        /// </summary>
        /// <param name="text">프로세스 명</param>
        /// <returns>ResultMapModel</returns>
        public ResultMapModel WriteLine(string data)
        {
            try
            {
                _client.WriteLine(data);
                return new ResultMapModel { ResultId = "0x00", ResultMessage = "Succes" };
            }
            catch (Exception ex)
            {
                return new ResultMapModel { ResultId = "0x01", ResultMessage = ex.Message };
            }
        }

        /// <summary>
        /// 바이트 형식의 데이터를 전송합니다.
        /// </summary>
        /// <param name="buffer">전송 데이터</param>
        /// <param name="offset">오프셋</param>
        /// <param name="count">전송크기</param>
        /// <returns>ResultMapModel</returns>
        public ResultMapModel Write(byte[] buffer, int offset, int count)
        {
            try
            {
                _client.Write(buffer, offset, count);
                return new ResultMapModel { ResultId = "0x00", ResultMessage = "Succes" };
            }
            catch (Exception ex)
            {
                return new ResultMapModel { ResultId = "0x01", ResultMessage = ex.Message };
            }
        }

        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!_isRead)
                return;

            _push(new object());
            throw new NotImplementedException();
        }

        private void OnPinChanged(object sender, SerialPinChangedEventArgs e)
        {
            if (!_isRead)
                return;

            throw new NotImplementedException();
        }

        private void OnErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            if (!_isRead)
                return;

            throw new NotImplementedException();
        }

        private void OnDisposed(object sender, EventArgs e)
        {
            if (!_isRead)
                return;
            
            throw new NotImplementedException();
        }
    }
}