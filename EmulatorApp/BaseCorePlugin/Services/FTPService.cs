using BaseCorePlugin.Contracts;
using BaseCorePlugin.Model;
using FluentFTP;
using System;
using System.Net;

namespace BaseCorePlugin.Services
{
    public class FTPService : IFTPService
    {
        public string _host = string.Empty;
        public string Host
        {
            get => _host;
            set => _host = string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        public string _user = string.Empty;
        public string User
        {
            get => _user;
            set => _user = string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        public string _password = string.Empty;
        public string Password
        {
            get => _password;
            set => _password = string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        public FtpClient _client;
        public bool State { get => _client.IsConnected; }

        /// <summary>
        /// FTP 접속을 시작합니다.
        /// </summary>
        /// <param name="host">FTP 호스트 서버</param>
        /// <param name="user">FTP 접속 계정</param>
        /// <param name="password">FTP 접속 비밀번호</param> 
        /// <returns>ResultMapModel</returns>
        public ResultMapModel Open(string host, string user, string password)
        {
            try
            {
                Host = host;
                User = user;
                Password = password;
                _client = new FtpClient(_host);
                _client.Credentials = new NetworkCredential(_user, _password);
                _client.Connect();
                return new ResultMapModel { ResultId = "0x00", ResultMessage = "Succes" };
            }
            catch (Exception ex)
            {
                return new ResultMapModel { ResultId = "0x01", ResultMessage = ex.Message };
            }
        }

        /// <summary>
        /// FTP 접속을 종료합니다.
        /// </summary>
        /// <returns>ResultMapModel</returns>
        public ResultMapModel Close()
        {
            try
            {
                _client.Disconnect();
                return new ResultMapModel { ResultId = "0x00", ResultMessage = "Succes" };
            }
            catch (Exception ex)
            {
                return new ResultMapModel { ResultId = "0x01", ResultMessage = ex.Message };
            }
        }

        /// <summary>
        /// FTP서버에서 파일을 내려받습니다.
        /// </summary>
        /// <param name="localPaht">로컬 파일경로.</param>
        /// <param name="remotePath">원격 파일경로.</param>
        /// <returns>ResultMapModel</returns>
        public ResultMapModel DownLoad(string localPaht, string remotePath)
        {
            try
            {
                var checkedSum = _client.DownloadFile(localPaht, remotePath, FtpLocalExists.Overwrite, FtpVerify.OnlyChecksum);
                return new ResultMapModel { ResultId = "0x00", ResultMessage = "Succes" };
            }
            catch (Exception ex)
            {
                return new ResultMapModel { ResultId = "0x01", ResultMessage = ex.Message };
            }
        }
    }
}
