using BaseCorePlugin.Model;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BaseCorePlugin.Contracts;
using System.Net.Http.Headers;

namespace BaseCorePlugin.Services
{
    public class HTTPService : IHTTPService
    {
        /// <summary>
        /// GET HTTP 메시지를 요청합니다.
        /// </summary>
        /// <param name="uri">요청 uri.</param>
        /// <param name="token">인증 token.</param>
        /// <returns>ResultMapModel</returns>
        public async Task<ResultMapModel> GetRequest(string uri, string token)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                var stream = await client.GetStringAsync(new Uri(uri));
                return new ResultMapModel { ResultId = "0x00", ResultMessage = stream };
            }
            catch (Exception ex)
            {
                return new ResultMapModel { ResultId = "0x01", ResultMessage = ex.Message };
            }
        }

        /// <summary>
        /// POST HTTP 메시지를 요청합니다.
        /// </summary>
        /// <param name="uri">요청 uri.</param>
        /// <param name="body">요청 메시지</param>
        /// <param name="token">인증 token.</param>
        /// <returns>ResultMapModel</returns>
        public async Task<ResultMapModel> PostRequest(string uri, string body, string token)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
                var httpContent = new StringContent(body, Encoding.UTF8, "application/json");
                var stream = await client.PostAsync(new Uri(uri), httpContent);
                return new ResultMapModel { ResultId = "0x00", ResultMessage = stream.Content.ToString() };
            }
            catch (Exception ex)
            {
                return new ResultMapModel { ResultId = "0x01", ResultMessage = ex.Message };
            }
        }
    }
}
