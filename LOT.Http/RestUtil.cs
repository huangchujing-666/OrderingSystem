using OrderingSystem.Http.Mos;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace OrderingSystem.Http
{
    /// <summary>
    /// http请求辅助类
    /// </summary>
    public static class RestUtil
    {
        private static readonly HttpClient m_Client;

        static RestUtil()
        {
            m_Client = new HttpClient(GetClientHandler());
        }

        #region   扩展方法

        /// <summary>
        /// 同步的请求方式
        /// </summary>
        /// <param name="request">请求的参数</param>
        /// <param name="client"></param>
        /// <returns>自定义的Response结果</returns>
        public static Task<HttpResponseMessage> RestSend(this OsHttpRequest request, HttpClient client = null)
        {
            return RestSend(request, HttpCompletionOption.ResponseContentRead, CancellationToken.None, client);
        }

        /// <summary>
        /// 同步的请求方式
        /// </summary>
        /// <param name="request">请求的参数</param>
        /// <param name="completionOption"></param>
        /// <param name="client"></param>
        /// <returns>自定义的Response结果</returns>
        public static Task<HttpResponseMessage> RestSend(this OsHttpRequest request,
            HttpCompletionOption completionOption, HttpClient client = null)
        {
            return RestSend(request, completionOption, CancellationToken.None, client);
        }

        /// <summary>
        /// 同步的请求方式
        /// </summary>
        /// <param name="request">请求的参数</param>
        /// <param name="completionOption"></param>
        /// <param name="token"></param>
        /// <param name="client"></param>
        /// <returns>自定义的Response结果</returns>
        public static Task<HttpResponseMessage> RestSend(this OsHttpRequest request,
            HttpCompletionOption completionOption,
            CancellationToken token,
            HttpClient client = null)
        {
            return (client ?? m_Client).RestSend(request, completionOption, token);
        }

        #endregion


        /// <summary>
        /// 配置请求处理类
        /// </summary>
        /// <returns></returns>
        private static HttpClientHandler GetClientHandler()
        {
            var reqHandler = new HttpClientHandler();

            reqHandler.AllowAutoRedirect = true;
            reqHandler.MaxAutomaticRedirections = 5;
            reqHandler.UseCookies = true;

            return reqHandler;
        }
    }
}